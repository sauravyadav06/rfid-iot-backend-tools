using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using MQTTnet.Client.Options;

namespace PSL.MqttProcessor

{
    namespace PSL.MqttProcessor
    {
        namespace PSL.MqttProcessor
        {
            public class MqttProcessor
            {
                private readonly IMqttClient mqttClient;
                private readonly MqttClientOptions options;
                private readonly ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
                private readonly AutoResetEvent queueSignal = new AutoResetEvent(false);
                private readonly CancellationTokenSource cts = new CancellationTokenSource();
                public Dictionary<string, string> tabToReaderDeviceMapping = new Dictionary<string, string>();
                public Dictionary<string, string> readerToTabdeviceMapping = new Dictionary<string, string>();
                public Dictionary<string, string> TopicMapping = new Dictionary<string, string>();
                public Dictionary<string, string> tagMapping = new Dictionary<string, string>();
                private static System.Timers.Timer _timer = null;
                private static readonly HttpClient _httpClient = new HttpClient();
                private string syncdatetime = null;

                private readonly string broker;
                private readonly int port;
                private readonly string apiURL;
                private readonly int minTagCount;
                private string subscribeTopic;

                public bool IsConnected { get; set; }

                public event Action<string> OnMessageProcessed;
                public event Action<string> OnConnectionStatusChanged;
                public event Action<int> OnThreadCountUpdated;
                
                public MqttProcessor(string broker, int port, string apiUrl,int minTagCount)
                {
                    this.broker = broker;
                    this.port = port;
                    this.minTagCount = minTagCount;
                    this.apiURL = apiUrl;

                    

                    mqttClient = new MqttFactory().CreateMqttClient();
                    options = CreateMqttOptions();

                    InitializeEventHandlers();
                    Task.Run(async () => await FetchApiData());
                    _timer = new System.Timers.Timer(60000);
                    _timer.Elapsed += async (sender, e) => await FetchApiData();
                    _timer.AutoReset = true;
                    _timer.Start();

                    ConnectAsync().ConfigureAwait(false);
                }

                private async Task FetchApiData()
                {
                    //string apiUrl = syncdatetime == null
                    //    ? "http://192.168.0.172/wmsapi/WMS/GetAllServiceData?dateTime="
                    //    : $"http://192.168.0.172/wmsapi/WMS/GetAllServiceData?dateTime={syncdatetime}";

                    string URL = syncdatetime == null
                        ? apiURL
                        : apiURL + syncdatetime;

                    try
                    {
                        using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
                        {
                            HttpResponseMessage response = await client.GetAsync(URL);
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = await response.Content.ReadAsStringAsync();
                                var data = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                                if (data.status && data.data != null)
                                {
                                    if (data.data.MapDevice.Count > 0 || data.data.Topics.Count > 0 || data.data.AssetDetails.Count > 0)
                                    {
                                        syncdatetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00.000");
                                        foreach (var d in data.data.MapDevice)
                                        {
                                            if (!tabToReaderDeviceMapping.ContainsKey(d.TabDeviceID))
                                            {
                                                tabToReaderDeviceMapping.Add(d.TabDeviceID, d.ReaderDeviceID);
                                                Console.WriteLine("countdevice1: " + tabToReaderDeviceMapping.Count);
                                            }
                                            if (!readerToTabdeviceMapping.ContainsKey(d.ReaderDeviceID))
                                            {
                                                readerToTabdeviceMapping.Add(d.ReaderDeviceID, d.TabDeviceID);
                                                Console.WriteLine("countdevice2: " + readerToTabdeviceMapping.Count);
                                            }
                                        }

                                        foreach (var t in data.data.Topics)
                                        {
                                            if (!TopicMapping.ContainsKey(t.Title))
                                            {
                                                TopicMapping.Add(t.Title, t.TopicName);
                                                Console.WriteLine("counttopic1: " + TopicMapping.Count);
                                            }
                                        }

                                        foreach (var a in data.data.AssetDetails)
                                        {
                                            if (!tagMapping.ContainsKey(a.TagID))
                                            {
                                                tagMapping.Add(a.TagID, a.TagName);
                                                Console.WriteLine("counttag1: " + tagMapping.Count);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"API Fetch Error: {ex.Message}");
                    }
                }

                private MqttClientOptions CreateMqttOptions()
                {
                    return new MqttClientOptionsBuilder()
                        .WithClientId(Guid.NewGuid().ToString())
                        .WithTcpServer(broker, port)
                        .WithCleanSession()
                        .WithTimeout(TimeSpan.FromSeconds(10)) // Prevent indefinite waits
                        .WithKeepAlivePeriod(TimeSpan.FromSeconds(15))
                        .Build();
                }

                private void InitializeEventHandlers()
                {
                    mqttClient.ConnectedAsync += async e => await HandleConnectedAsync();
                    mqttClient.DisconnectedAsync += async e => await HandleDisconnectedAsync();
                    mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
                }

                private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
                {
                    string message = Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment.ToArray());
                    Console.WriteLine($"🔹 MQTT Message Received: {message}");

                    messageQueue.Enqueue(message);
                    queueSignal.Set();

                    OnMessageProcessed?.BeginInvoke(message, null, null);

                    try
                    {
                        TagLoggerResponse messageData = JsonConvert.DeserializeObject<TagLoggerResponse>(message);
                        if (messageData != null)
                        {
                            ThreadPool.QueueUserWorkItem(state => ProcessMqttData(messageData));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" Error deserializing MQTT message: {ex.Message}");
                    }

                    return Task.CompletedTask;
                }

                private async Task HandleConnectedAsync()
                {
                    IsConnected = true;
                    OnConnectionStatusChanged?.BeginInvoke($"Connected to {broker}:{port}", null, null);

                    if (!string.IsNullOrEmpty(subscribeTopic))
                    {
                        await SubscribeToTopic(subscribeTopic);
                    }
                }

                private async Task HandleDisconnectedAsync()
                {
                    IsConnected = false;
                    OnConnectionStatusChanged?.BeginInvoke("MQTT Disconnected! Reconnecting...", null, null);
                    await Task.Delay(ExponentialBackoff());
                    await ReconnectAsync();
                }

                public async Task ConnectAsync()
                {
                    try
                    {
                        await mqttClient.ConnectAsync(options, cts.Token);
                        IsConnected = true;
                        OnConnectionStatusChanged?.BeginInvoke($"Connected to {broker}:{port}", null, null);
                    }
                    catch (Exception ex)
                    {
                        IsConnected = false;
                        OnConnectionStatusChanged?.BeginInvoke($"Connection failed: {ex.Message}", null, null);
                        throw;
                    }
                }

                private async Task ReconnectAsync()
                {
                    int retryCount = 0;
                    while (!mqttClient.IsConnected && !cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            await mqttClient.DisconnectAsync(); // Ensure clean disconnect
                            await mqttClient.ConnectAsync(options, cts.Token); // Try reconnecting

                            //  SUCCESS: Update status and exit loop
                            IsConnected = true;
                            OnConnectionStatusChanged?.BeginInvoke($"✅ Reconnected to {broker}:{port}", null, null);
                            return;  // Exit once reconnected
                        }
                        catch (Exception ex)
                        {
                            retryCount++;
                            OnConnectionStatusChanged?.BeginInvoke($"⚠️ Connection lost,trying to reconnect", null, null);

                            // ✅ Wait before retrying (use exponential backoff to avoid spamming)
                            await Task.Delay(ExponentialBackoff());
                        }
                    }
                }


                private int ExponentialBackoff()
                {
                    return new Random().Next(5000, 20000);
                }

                private async void ProcessMqttData(TagLoggerResponse data)
                {
                    if (cts.Token.IsCancellationRequested) return;
                    queueSignal.WaitOne();
                    if (cts.Token.IsCancellationRequested) return;

                    if (data == null || string.IsNullOrEmpty(data.pubDeviceID) || string.IsNullOrEmpty(data.messageType))
                    {
                        Console.WriteLine("Invalid data received.");
                        return;
                    }

                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"[Thread {threadId}] Processing message from {data.pubDeviceID}");

                    string messageJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                    Console.WriteLine($"JSON Output: {messageJson}");

                    if (!readerToTabdeviceMapping.TryGetValue(data.pubDeviceID, out string tabdevice))
                    {
                        Console.WriteLine($"No mapping found for PubDeviceID: {data.pubDeviceID}, skipping publish.");
                        return;
                    }

                    int tagCount = 0;
                    string message = null;
                    string baseTopic;

                    switch (data.messageType)
                    {
                        case "DataLogger":
                            List<taginfo> modifiedTags = new List<taginfo>();
                            if (data.data is JObject)
                            {
                                tagdata tags = ((JObject)data.data).ToObject<tagdata>();
                            
                                   

                                // Update tag names
                                foreach (var tag in tags.tagDetails)
                                {
                                    if (tagMapping.ContainsKey(tag.tagID))
                                    {
                                        tag.tagName = tagMapping[tag.tagID];
                                    }
                                    tagCount = tag.count;

                                    if (tagCount > minTagCount)
                                    {
                                        modifiedTags.Add(tag);
                                    }
                                }


                                //data.data = tags;
                                //modifiedTags = tags.tagDetails;
                                data.data = new tagdata { tagDetails = modifiedTags };
                            }

                            TagLoggerResponse modifiedData = new TagLoggerResponse
                            {
                                messageType = data.messageType,
                                pubDeviceID = data.pubDeviceID,
                                subDeviceID = data.subDeviceID,
                                data = new tagdata { tagDetails = modifiedTags }
                            };

                            message = JsonConvert.SerializeObject(modifiedData);
                            baseTopic = TopicMapping["Command"] + tabdevice + "/display";
                            break;

                        case "DataConfig":
                            message = JsonConvert.SerializeObject(data);
                            baseTopic = TopicMapping["Command"] + tabdevice + "/config";
                            break;

                        default:
                            Console.WriteLine($"Unknown MessageType '{data.messageType}', skipping publish.");
                            return;
                    }

                    // ✅ Only publish if the tag count is greater than the configured value
                    
                        await PublishMqttMessage(baseTopic, message);
                        Console.WriteLine($"✅ Published to [{baseTopic}]: {message}");
                    

                    LogThreadCount();
                    OnMessageProcessed?.BeginInvoke($"Processed by Thread {threadId}: {message}", null, null);
                }



                public async Task PublishMqttMessage(string topic, string message)
                {
                    if (!mqttClient.IsConnected)
                    {
                        OnConnectionStatusChanged?.BeginInvoke("Publish failed: MQTT not connected. Attempting reconnect...", null, null);
                        await ReconnectAsync();
                        if (!mqttClient.IsConnected)
                        {
                            OnConnectionStatusChanged?.BeginInvoke("Publish aborted: Reconnection failed.", null, null);
                            return;
                        }
                    }

                    var messageBuilder = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(message)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    try
                    {
                        await mqttClient.PublishAsync(messageBuilder, cts.Token);
                        Console.WriteLine($"Successfully published to {topic}");

                        //  Use BeginInvoke to notify UI asynchronously that this message was published
                        OnMessageProcessed?.BeginInvoke($"Published Successfully: {message}", null, null);
                    }
                    catch (MqttCommunicationException ex)
                    {
                        OnConnectionStatusChanged?.BeginInvoke($"Publish failed: MQTT communication error - {ex.Message}", null, null);
                        await ReconnectAsync();
                    }
                    catch (Exception ex)
                    {
                        OnConnectionStatusChanged?.BeginInvoke($"Publish failed: {ex.Message}", null, null);
                    }
                }


                public async Task SubscribeToTopic(string topic)
                {
                    subscribeTopic = topic;

                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        OnConnectionStatusChanged?.BeginInvoke("Communication Error: MQTT Not Connected", null, null);
                        return;
                    }

                    try
                    {
                        await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                            .WithTopicFilter(topic)
                            .Build(), cts.Token);
                        OnConnectionStatusChanged?.BeginInvoke($"Subscribed to: {topic}", null, null);
                    }
                    catch (Exception ex)
                    {
                        OnConnectionStatusChanged?.BeginInvoke($"Subscription Error: {ex.Message}", null, null);
                    }
                }

                public int GetThreadCount()
                {
                    return Process.GetCurrentProcess().Threads.Count;
                }

                private void LogThreadCount()
                {
                    int threadCount = GetThreadCount();
                    Console.WriteLine($"[THREAD COUNT] Active Threads: {threadCount}");
                    OnThreadCountUpdated?.BeginInvoke(threadCount, null, null);
                }

                public void Stop()
                {
                    cts.Cancel();
                    queueSignal.Set();
                    mqttClient?.DisconnectAsync().Wait();
                    _timer?.Stop();
                    _timer?.Dispose();

                }
            }
        }
    }
}






