using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MQTT_Mongodb_Connection
{
    public partial class Form1 : Form
    {
        private MqttMongoBridge _bridge = new MqttMongoBridge();
        private bool _dumpingActive = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btnTestConn.Click += BtnTestConn_Click;
            btnReload.Click += BtnReload_Click;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Auto-fill fields from config via bridge
            txtMongoConn.Text = _bridge.MongoConnectionString;
            txtMongoDb.Text = _bridge.MongoDbName;
            txtMongoCol.Text = _bridge.MongoCollectionName;

            // Optionally: disable editing if you want
            // txtMongoConn.ReadOnly = true;
            // txtMongoDb.ReadOnly = true;
            // txtMongoCol.ReadOnly = true;

            // Connect to MQTT broker (as before)
            await _bridge.StartAsync(OnMessageReceived, OnConnectionChanged);
        }

        // TEST button: test MongoDB and start dumping if successful
        private async void BtnTestConn_Click(object sender, EventArgs e)
        {
            // Take textbox values in case user edits
            _bridge.UpdateConfig(
                txtMongoConn.Text.Trim(),
                txtMongoDb.Text.Trim(),
                txtMongoCol.Text.Trim(),
                _bridge.MqttBroker,
                _bridge.MqttPort,
                _bridge.MqttTopic);

            bool ok = await _bridge.TestMongoConnectionAsync();
            if (ok)
            {
                lblConnStatus.Text = "Status: DB Connected";
                MessageBox.Show("MongoDB connection established!", "DB Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartDumping();
            }
            else
            {
                lblConnStatus.Text = "Status: DB Connection Failed";
                MessageBox.Show("Failed to connect to MongoDB. Check your settings.", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Start dumping messages to grid and DB
        private void StartDumping()
        {
            if (_dumpingActive) return;
            _dumpingActive = true;
            lblDumpStatus.Text = "Dump Status: Running";
            // Messages will arrive in OnMessageReceived handler
        }

        // Handler for new MQTT messages
        private void OnMessageReceived(string topic, DateTime receivedAt, string payload)
        {
            if (!this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                dataGridViewMessages.Rows.Insert(0, topic, receivedAt.ToString("yyyy-MM-dd HH:mm:ss"), payload);
                if (dataGridViewMessages.Rows.Count > 100)
                    dataGridViewMessages.Rows.RemoveAt(dataGridViewMessages.Rows.Count - 1);
            }));
        }

        // Handler for MQTT connection state change
        private void OnConnectionChanged(bool connected)
        {
            this.BeginInvoke(new Action(() =>
            {
                lblConnStatus.Text = connected ? "Status: MQTT Connected" : "Status: MQTT Disconnected, retrying...";
                if (connected)
                    StartDumping();
                else
                    _dumpingActive = false;
            }));
        }

        // Reload button: refresh the grid from recent DB data
        private async void BtnReload_Click(object sender, EventArgs e)
        {
            var docs = await _bridge.GetRecentMessagesAsync(100);
            dataGridViewMessages.Rows.Clear();
            foreach (var doc in docs)
            {
                string topic = doc.GetValue("topic", "").ToString();
                string receivedAt = doc.GetValue("received_at", "").ToString();
                string payload = doc.GetValue("message", "").ToString();
                dataGridViewMessages.Rows.Add(topic, receivedAt, payload);
            }
        }
    }
}