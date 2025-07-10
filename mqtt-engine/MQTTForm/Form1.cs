using Newtonsoft.Json;
using PSL.MqttProcessor;
using PSL.MqttProcessor.PSL.MqttProcessor;
using PSL.MqttProcessor.PSL.MqttProcessor.PSL.MqttProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MQTTForm
{
    public partial class Form1 : Form
    {
        private static MqttProcessor mqttProcessor;
        // Fields for pause/resume features
        private bool isSubscriberPaused = false;
        private bool isPublisherPaused = false;
        private string apiURL = ConfigurationManager.AppSettings["fetchURL"].ToString();
        private int minTagCount = int.Parse(ConfigurationManager.AppSettings["tagAcessProperty"]);

        public Form1()
        {
            InitializeComponent();
            // Wire up event handlers for the checkboxes and listbox double-click events.
            PauseSubscriber.CheckedChanged += chkPauseSubscriber_CheckedChanged;
            PausePublisher.CheckedChanged += chkPausePublisher_CheckedChanged;
            listBox1.MouseDoubleClick += listBox1_MouseDoubleClick;
            lstPub.MouseDoubleClick += lstPub_MouseDoubleClick;
            btnSearchSubscriber.Click += btnSearchSubscriber_Click;
            btnResetSearch.Click += btnResetSearch_Click;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (mqttProcessor != null && mqttProcessor.IsConnected)
            {
                MessageBox.Show("MQTT is already connected!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string broker = txtBroker.Text.Trim();
            if (string.IsNullOrEmpty(broker))
            {
                MessageBox.Show("Please enter a broker address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPort.Text.Trim(), out int port))
            {
                MessageBox.Show("Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                mqttProcessor = new MqttProcessor(broker, port, apiURL, minTagCount);  // Only create if not already initialized


                mqttProcessor.OnConnectionStatusChanged += UpdateConnectionStatus;
                mqttProcessor.OnMessageProcessed += ProcessedMessageReceived;
                mqttProcessor.OnThreadCountUpdated += UpdateThreadCount;



                //await mqttProcessor.ConnectAsync();  // ✅ Await the connection

                //if (!mqttProcessor.IsConnected)
                //{
                //    MessageBox.Show("MQTT connection failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                btnStart.Enabled = false;
                btnclear.Enabled = true;
                MessageBox.Show("Connected to MQTT Broker!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // private string autoPublishTopic = null; // Store the topic for continuous publishing

        private async void btnPublish_Click(object sender, EventArgs e)
        {
            if (mqttProcessor == null || !mqttProcessor.IsConnected)
            {
                MessageBox.Show("MQTT is not connected! Please start the connection first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string topic = txtPubTopic.Text.Trim();
            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("Please enter a topic to publish.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "📡 Manual Publish Test"; // ✅ Test message

            //  Publish message
            await mqttProcessor.PublishMqttMessage(topic, message);

            // Only update Publisher ListBox (lstPub) if NOT paused
            if (!isPublisherPaused)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        lstPub.Items.Insert(0, $" Published to [{topic}]: {message}");
                    }));
                }
                else
                {
                    lstPub.Items.Insert(0, $" Published to [{topic}]: {message}");
                }
            }
        }



        private void UpdateConnectionStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    conlbl.Text = status;
                    conlbl.ForeColor = Color.Green;
                }));
            }
            else
            {
                conlbl.Text = status;
            }
        }

        private void UpdateThreadCount(int threadCount)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblThreadCount.Text = $"Threads Running: {threadCount}"));
            }
            else
            {
                lblThreadCount.Text = $"Threads Running: {threadCount}";
            }
        }

        // Event to display processed messages.
        // Both subscriber and publisher tabs are updated, provided they're not paused.
        private void ProcessedMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    // ✅ Only update Subscriber ListBox (listBox1)
                    if (!isSubscriberPaused && !message.Contains("Published Successfully"))
                    {
                        listBox1.Items.Insert(0, $"📥 Received: {message}");

                        if (listBox1.Items.Count > 500)
                            listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                    }

                    // ✅ Only update Publisher ListBox (lstPub) for published messages
                    if (!isPublisherPaused && message.Contains("Published Successfully"))
                    {
                        lstPub.Items.Insert(0, $"🔄 {message}");

                        if (lstPub.Items.Count > 500)
                            lstPub.Items.RemoveAt(lstPub.Items.Count - 1);
                    }
                }));
            }
        }





        // Stop MQTT when Form Closes.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mqttProcessor?.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (mqttProcessor == null)
            {
                MessageBox.Show("MQTT is not running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStart.Enabled = true;
            btnclear.Enabled = false;
            mqttProcessor?.Stop();
            MessageBox.Show("MQTT Disconnected!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnSub_Click(object sender, EventArgs e)
        {
            string subscribeTopic = txtSubscribeTopic.Text.Trim();

            if (string.IsNullOrEmpty(subscribeTopic))
            {
                MessageBox.Show("Please enter a topic to subscribe!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mqttProcessor == null || !mqttProcessor.IsConnected)
            {
                MessageBox.Show("MQTT is not connected! Please start the connection first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await mqttProcessor.SubscribeToTopic(subscribeTopic);
            MessageBox.Show($" Subscribed to {subscribeTopic}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Console.WriteLine($" Subscribed to topic: {subscribeTopic}");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            // Unchanged
        }

        private void txtPubTopic_TextChanged(object sender, EventArgs e)
        {
            // Unchanged
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        //private void btnClearPub_Click(object sender, EventArgs e)
        //{
        //    lstPub.Items.Clear();
        //}


        // CheckBox event handler to pause/resume subscriber tab updates.
        private void chkPauseSubscriber_CheckedChanged(object sender, EventArgs e)
        {
            isSubscriberPaused = PauseSubscriber.Checked;
            PauseSubscriber.Text = isSubscriberPaused ? "Resume Subscriber" : "Pause Subscriber";

            if (isSubscriberPaused)
            {
                MessageBox.Show("Subscriber updates are paused. New messages will not be shown until resumed.",
                    "Subscriber Paused", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        // CheckBox event handler to pause/resume publisher tab updates.
        private void chkPausePublisher_CheckedChanged(object sender, EventArgs e)
        {
            isPublisherPaused = PausePublisher.Checked;
            PausePublisher.Text = isPublisherPaused ? "Resume Publisher" : "Pause Publisher";

            if (isPublisherPaused)
            {
                MessageBox.Show("Publisher updates are paused. New messages will not be shown until resumed.",
                    "Publisher Paused", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Handles double-click events on subscriber listBox items to show message details.
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show(listBox1.SelectedItem.ToString(), "Subscriber Message Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // (Optional) Handles double-click events on publisher listBox items to show message details.
        private void lstPub_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstPub.SelectedItem != null)
            {
                MessageBox.Show(lstPub.SelectedItem.ToString(), "Publisher Message Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnSearchSubscriber_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchSubscriber.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter text to search.", "Search Subscriber", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Search only within listBox1 (current messages)
            List<string> searchResults = new List<string>();

            foreach (var item in listBox1.Items)
            {
                if (item.ToString().ToLower().Contains(searchText))
                {
                    searchResults.Add(item.ToString());
                }
            }

            // ✅ Show search results in listBox1
            listBox1.Items.Clear();
            foreach (string result in searchResults)
            {
                listBox1.Items.Add(result);
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No matching messages found.", "Search Subscriber", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearchSubscriber.Text = ""; //  Clear search input

            // Reset: Remove search filter and restore only currently available messages
            //    listBox1.Items.Clear();
        }

        private void btnClearPub_Click_1(object sender, EventArgs e)
        {
            lstPub.Items.Clear();
        }

        private void btnSearchSubscriber_Click_1(object sender, EventArgs e)
        {

        }
    }
}







