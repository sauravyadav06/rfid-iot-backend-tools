using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using System.Linq;

namespace MQTT_Mongodb_Connection
{
    public class MqttMongoBridge
    {
        public string MongoConnectionString { get; set; }
        public string MongoDbName { get; set; }
        public string MongoCollectionName { get; set; }
        public string MqttBroker { get; set; }
        public int MqttPort { get; set; }
        public string MqttTopic { get; set; }

        private IMqttClient _mqttClient;
        private MongoClient _mongoClient;
        private IMongoDatabase _db;
        private IMongoCollection<BsonDocument> _collection;
        private CancellationTokenSource _cts;

        // Batch/buffer members
        private readonly List<BsonDocument> _batchBuffer = new List<BsonDocument>();
        private readonly object _batchLock = new object();
        private const int BatchSize = 100;
        private Timer _batchTimer;

        // Callbacks
        private Action<string, DateTime, string> _onMessage;
        private Action<bool> _onConnectionChanged;

        public MqttMongoBridge()
        {
            MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"] ?? "mongodb://localhost:27017";
            MongoDbName = ConfigurationManager.AppSettings["MongoDatabase"] ?? "mqtt_data";
            MongoCollectionName = ConfigurationManager.AppSettings["MongoCollection"] ?? "messages";
            MqttBroker = ConfigurationManager.AppSettings["MqttBroker"] ?? "localhost";
            MqttPort = int.TryParse(ConfigurationManager.AppSettings["MqttPort"], out var port) ? port : 1883;
            MqttTopic = ConfigurationManager.AppSettings["MqttTopic"] ?? "#";
        }

        public void UpdateConfig(string mongoConn, string mongoDb, string mongoCol, string mqttBroker, int mqttPort, string mqttTopic)
        {
            MongoConnectionString = mongoConn;
            MongoDbName = mongoDb;
            MongoCollectionName = mongoCol;
            MqttBroker = mqttBroker;
            MqttPort = mqttPort;
            MqttTopic = mqttTopic;
        }

        public async Task<bool> TestMongoConnectionAsync()
        {
            try
            {
                var testClient = new MongoClient(MongoConnectionString);
                var testDb = testClient.GetDatabase(MongoDbName);
                var testCol = testDb.GetCollection<BsonDocument>(MongoCollectionName);
                await testCol.CountDocumentsAsync(FilterDefinition<BsonDocument>.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> StartAsync(Action<string, DateTime, string> onMessage, Action<bool> onConnectionChanged = null)
        {
            _onMessage = onMessage;
            _onConnectionChanged = onConnectionChanged;
            _cts = new CancellationTokenSource();

            // Prepare MongoDB
            _mongoClient = new MongoClient(MongoConnectionString);
            _db = _mongoClient.GetDatabase(MongoDbName);
            _collection = _db.GetCollection<BsonDocument>(MongoCollectionName);

            // Start MQTT connection and subscription
            return await ConnectAndSubscribeAsync();
        }

        private async Task<bool> ConnectAndSubscribeAsync()
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(MqttBroker, MqttPort)
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += async arg =>
            {
                string msgTopic = arg.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);

                // --- Recommended way: Store both UTC and IST ---
                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime utcNow = DateTime.UtcNow;
                DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);

                var doc = new BsonDocument
                {
                    { "topic", BsonValue.Create(msgTopic) },
                    { "received_at_utc", new BsonDateTime(utcNow) }, // UTC for queries/comparison
                    { "received_at_ist", istNow.ToString("yyyy-MM-dd HH:mm:ss") }, // IST as string for display
                    { "message", TryParseBson(payload) ?? BsonValue.Create(payload) }
                };

                bool flushNow = false;
                lock (_batchLock)
                {
                    _batchBuffer.Add(doc);
                    if (_batchBuffer.Count >= BatchSize)
                        flushNow = true;
                }
                if (flushNow)
                    await FlushBatchAsync();

                _onMessage?.Invoke(msgTopic, istNow, payload); // For UI, pass IST
            };

            _mqttClient.DisconnectedAsync += async e =>
            {
                await FlushBatchAsync();
                _onConnectionChanged?.Invoke(false);
                _ = Task.Run(async () => await ReconnectLoopAsync());
            };

            // Timer: flush every 2 minutes
            _batchTimer?.Dispose();
            _batchTimer = new Timer(async _ =>
            {
                await FlushBatchAsync();
            }, null, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2));

            try
            {
                await _mqttClient.ConnectAsync(options, _cts.Token);
            }
            catch
            {
                _onConnectionChanged?.Invoke(false);
                _ = Task.Run(async () => await ReconnectLoopAsync());
                return false;
            }

            var subscribeResult = await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(MqttTopic).Build());
            bool ok = subscribeResult.Items != null && subscribeResult.Items.All(i =>
                i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS0 ||
                i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS1 ||
                i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS2);

            _onConnectionChanged?.Invoke(ok);
            return ok;
        }

        private async Task ReconnectLoopAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(2000);
                    var mqttFactory = new MqttFactory();
                    _mqttClient = mqttFactory.CreateMqttClient();

                    var options = new MqttClientOptionsBuilder()
                        .WithTcpServer(MqttBroker, MqttPort)
                        .Build();

                    _mqttClient.ApplicationMessageReceivedAsync += async arg =>
                    {
                        string msgTopic = arg.ApplicationMessage.Topic;
                        string payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);

                        TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime utcNow = DateTime.UtcNow;
                        DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);

                        var doc = new BsonDocument
                        {
                            { "topic", BsonValue.Create(msgTopic) },
                            { "received_at_utc", new BsonDateTime(utcNow) },
                            { "received_at_ist", istNow.ToString("yyyy-MM-dd HH:mm:ss") },
                            { "message", TryParseBson(payload) ?? BsonValue.Create(payload) }
                        };

                        bool flushNow = false;
                        lock (_batchLock)
                        {
                            _batchBuffer.Add(doc);
                            if (_batchBuffer.Count >= BatchSize)
                                flushNow = true;
                        }
                        if (flushNow)
                            await FlushBatchAsync();

                        _onMessage?.Invoke(msgTopic, istNow, payload);
                    };

                    _mqttClient.DisconnectedAsync += async e =>
                    {
                        await FlushBatchAsync();
                        _onConnectionChanged?.Invoke(false);
                        _ = Task.Run(async () => await ReconnectLoopAsync());
                    };

                    await _mqttClient.ConnectAsync(options, _cts.Token);
                    var subscribeResult = await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(MqttTopic).Build());
                    bool ok = subscribeResult.Items != null && subscribeResult.Items.All(i =>
                        i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS0 ||
                        i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS1 ||
                        i.ResultCode == MqttClientSubscribeResultCode.GrantedQoS2);

                    if (ok)
                    {
                        _onConnectionChanged?.Invoke(true);
                        break; // Successfully reconnected!
                    }
                }
                catch
                {
                    // ignore and retry
                }
            }
        }

        public async Task StopAsync()
        {
            _batchTimer?.Dispose();
            await FlushBatchAsync(); // Flush remaining buffer
            if (_mqttClient != null && _mqttClient.IsConnected)
                await _mqttClient.DisconnectAsync();
            _cts?.Cancel();
        }

        /// <summary>
        /// Flushes the current batch buffer to MongoDB (called on size or timer).
        /// </summary>
        public async Task FlushBatchAsync()
        {
            List<BsonDocument> toInsert = null;
            lock (_batchLock)
            {
                if (_batchBuffer.Count > 0)
                {
                    toInsert = new List<BsonDocument>(_batchBuffer);
                    _batchBuffer.Clear();
                }
            }
            if (toInsert != null && toInsert.Count > 0)
            {
                try { await _collection.InsertManyAsync(toInsert); }
                catch (Exception ex) { /* Optionally log error */ }
            }
        }

        public async Task<BsonDocument[]> GetRecentMessagesAsync(int limit = 100)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var sort = Builders<BsonDocument>.Sort.Descending("received_at_utc");
            var cursor = await _collection.Find(filter).Sort(sort).Limit(limit).ToListAsync();
            return cursor.ToArray();
        }

        private BsonDocument TryParseBson(string payload)
        {
            try
            {
                return BsonDocument.Parse(payload);
            }
            catch
            {
                return null;
            }
        }
    }
}