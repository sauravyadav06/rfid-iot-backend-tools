using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MqttLoadTester
{
    public partial class Form1 : Form
    {
        private IMqttClient mqttClient;
        private bool isPublishing = false;

        public Form1()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            LogToListBox("Form initialized.");
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Start button clicked!", "Debug");
            LogToListBox("Start button clicked.");

            string broker = txtBroker.Text.Trim();
            int port = int.TryParse(txtPort.Text.Trim(), out int p) ? p : 1883;

            LogToListBox($"Broker: '{broker}', Port: {port}");

            if (string.IsNullOrEmpty(broker))
            {
                LogToListBox("❌ No broker IP entered!");
                MessageBox.Show("Please enter a broker IP!", "Error");
                return;
            }

            try
            {
                LogToListBox($"Attempting to connect to {broker}:{port}...");
                mqttClient = new MqttFactory().CreateMqttClient();
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithTcpServer(broker, port)
                    .Build();

                mqttClient.ConnectedAsync += async args =>
                {
                    LogToListBox("✅ Connected to broker!");
                    await Task.CompletedTask;
                };

                await mqttClient.ConnectAsync(options);
                LogToListBox("ConnectAsync called.");

                isPublishing = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                LogToListBox("Starting load test...");
                StartLoadTest();
                //await Task.Run(() => StartLoadTest());
            }
            catch (Exception ex)
            {
                LogToListBox($"❌ Connection failed: {ex.Message}");
                MessageBox.Show($"Connection failed: {ex.Message}", "Error");
            }
        }

        private void StartLoadTest()
        {
            string baseDeviceID = "F8D10ED88DA4D6";
            LogToListBox("Inside StartLoadTest...");

            //while (isPublishing)
            //{
                for (int i = 1; i <= 10; i++)
                {
                    if (!isPublishing) break;

                    string deviceID = $"{baseDeviceID}{i:D2}";
                    string topic = $"dt/PSLWMS/rfid/logger/{deviceID}";

                    var message = new
                    {
                        messageType = "DataLogger",
                        pubDeviceID = deviceID,
                        subDeviceID = "07A646DC0AD22D03",
                        data = new
                        {
                            tagDetails = new List<object>
                            {
                                new { tagID = "99030000C6CF50534C202020", tagName = "", rssi = -38, antennaID = "2" }
                            }
                        }
                    };

                    string payload = JsonConvert.SerializeObject(message);
                    ThreadPool.QueueUserWorkItem(state => PublishMessage(topic, payload));
                    //await PublishMessage(topic, payload);
                    //await Task.Delay(300);
                }
            //}
            LogToListBox("Load test ended.");
        }

        private void PublishMessage(string topic, string message)
        {
            while (true)
            {
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    LogToListBox("⚠️ Client not connected!");
                    isPublishing = false;
                    return;
                }

                try
                {
                    var mqttMessage = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(System.Text.Encoding.UTF8.GetBytes(message))
                        .Build();

                    mqttClient.PublishAsync(mqttMessage);
                    LogToListBox($"📡 Published to {topic}");
                }
                catch (Exception ex)
                {
                    LogToListBox($"❌ Publish failed: {ex.Message}");
                }

                Thread.Sleep(500);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            LogToListBox("Stop button clicked.");
            isPublishing = false;
            if (mqttClient != null && mqttClient.IsConnected)
            {
                mqttClient.DisconnectAsync().Wait();
                LogToListBox("Disconnected.");
            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        // New method to add messages to lstLogs
        private void LogToListBox(string message)
        {
            if (lstLogs.InvokeRequired)
            {
                // If called from a background thread, use Invoke
                lstLogs.Invoke(new Action(() => lstLogs.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {message}")));
            }
            else
            {
                // If on the UI thread, add directly
                lstLogs.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {message}");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBroker_TextChanged(object sender, EventArgs e)
        {

        }
    }
}