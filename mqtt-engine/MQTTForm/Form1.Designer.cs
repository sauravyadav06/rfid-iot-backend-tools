namespace MQTTForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Search = new System.Windows.Forms.Label();
            this.btnResetSearch = new System.Windows.Forms.Button();
            this.btnSearchSubscriber = new System.Windows.Forms.Button();
            this.txtSearchSubscriber = new System.Windows.Forms.TextBox();
            this.PauseSubscriber = new System.Windows.Forms.CheckBox();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.lblThreadCount = new System.Windows.Forms.Label();
            this.conlbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPublishTopic = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubscribeTopic = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtBroker = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstPub = new System.Windows.Forms.ListBox();
            this.labelPubMessage = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PausePublisher = new System.Windows.Forms.CheckBox();
            this.btnClearPub = new System.Windows.Forms.Button();
            this.brokeradress = new System.Windows.Forms.Label();
            this.broker = new System.Windows.Forms.TextBox();
            this.labelPubTopic = new System.Windows.Forms.Label();
            this.txtPubTopic = new System.Windows.Forms.TextBox();
            this.btnPublish = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1549, 815);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1541, 786);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Subscriber";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 161);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1535, 622);
            this.panel2.TabIndex = 30;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(1535, 622);
            this.listBox1.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Controls.Add(this.Search);
            this.panel1.Controls.Add(this.btnResetSearch);
            this.panel1.Controls.Add(this.btnSearchSubscriber);
            this.panel1.Controls.Add(this.txtSearchSubscriber);
            this.panel1.Controls.Add(this.PauseSubscriber);
            this.panel1.Controls.Add(this.btnSub);
            this.panel1.Controls.Add(this.btnclear);
            this.panel1.Controls.Add(this.lblThreadCount);
            this.panel1.Controls.Add(this.conlbl);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPublishTopic);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSubscribeTopic);
            this.panel1.Controls.Add(this.txtPort);
            this.panel1.Controls.Add(this.txtBroker);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1535, 158);
            this.panel1.TabIndex = 29;
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Location = new System.Drawing.Point(945, 73);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(88, 17);
            this.Search.TabIndex = 45;
            this.Search.Text = "Search Here";
            // 
            // btnResetSearch
            // 
            this.btnResetSearch.Location = new System.Drawing.Point(1266, 98);
            this.btnResetSearch.Name = "btnResetSearch";
            this.btnResetSearch.Size = new System.Drawing.Size(75, 23);
            this.btnResetSearch.TabIndex = 44;
            this.btnResetSearch.Text = "Reset ";
            this.btnResetSearch.UseVisualStyleBackColor = true;
            // 
            // btnSearchSubscriber
            // 
            this.btnSearchSubscriber.Location = new System.Drawing.Point(1172, 97);
            this.btnSearchSubscriber.Name = "btnSearchSubscriber";
            this.btnSearchSubscriber.Size = new System.Drawing.Size(75, 25);
            this.btnSearchSubscriber.TabIndex = 43;
            this.btnSearchSubscriber.Text = "Lets Go";
            this.btnSearchSubscriber.UseVisualStyleBackColor = true;
            this.btnSearchSubscriber.Click += new System.EventHandler(this.btnSearchSubscriber_Click_1);
            // 
            // txtSearchSubscriber
            // 
            this.txtSearchSubscriber.Location = new System.Drawing.Point(841, 101);
            this.txtSearchSubscriber.Name = "txtSearchSubscriber";
            this.txtSearchSubscriber.Size = new System.Drawing.Size(315, 22);
            this.txtSearchSubscriber.TabIndex = 42;
            this.txtSearchSubscriber.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PauseSubscriber
            // 
            this.PauseSubscriber.AutoSize = true;
            this.PauseSubscriber.Location = new System.Drawing.Point(756, 27);
            this.PauseSubscriber.Name = "PauseSubscriber";
            this.PauseSubscriber.Size = new System.Drawing.Size(190, 21);
            this.PauseSubscriber.TabIndex = 41;
            this.PauseSubscriber.Text = "PauseResumeSubscriber";
            this.PauseSubscriber.UseVisualStyleBackColor = true;
            // 
            // btnSub
            // 
            this.btnSub.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSub.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSub.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSub.Location = new System.Drawing.Point(358, 84);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(130, 45);
            this.btnSub.TabIndex = 39;
            this.btnSub.Text = "Subscribe";
            this.btnSub.UseVisualStyleBackColor = false;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.Red;
            this.btnclear.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnclear.Location = new System.Drawing.Point(551, 13);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(130, 45);
            this.btnclear.TabIndex = 38;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreadCount.Location = new System.Drawing.Point(280, 135);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(125, 17);
            this.lblThreadCount.TabIndex = 37;
            this.lblThreadCount.Text = "Thread Running";
            // 
            // conlbl
            // 
            this.conlbl.AutoSize = true;
            this.conlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conlbl.Location = new System.Drawing.Point(1013, 20);
            this.conlbl.Name = "conlbl";
            this.conlbl.Size = new System.Drawing.Size(191, 25);
            this.conlbl.TabIndex = 36;
            this.conlbl.Text = "MQTT Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(516, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "Publish Topic";
            // 
            // txtPublishTopic
            // 
            this.txtPublishTopic.Enabled = false;
            this.txtPublishTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPublishTopic.Location = new System.Drawing.Point(511, 92);
            this.txtPublishTopic.Multiline = true;
            this.txtPublishTopic.Name = "txtPublishTopic";
            this.txtPublishTopic.Size = new System.Drawing.Size(312, 31);
            this.txtPublishTopic.TabIndex = 34;
            this.txtPublishTopic.Text = "PSL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(263, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 32;
            this.label1.Text = "Subscribe Topic";
            // 
            // txtSubscribeTopic
            // 
            this.txtSubscribeTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscribeTopic.Location = new System.Drawing.Point(15, 92);
            this.txtSubscribeTopic.Multiline = true;
            this.txtSubscribeTopic.Name = "txtSubscribeTopic";
            this.txtSubscribeTopic.Size = new System.Drawing.Size(337, 31);
            this.txtSubscribeTopic.TabIndex = 31;
            this.txtSubscribeTopic.Text = "dt/#";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(261, 23);
            this.txtPort.Multiline = true;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(91, 30);
            this.txtPort.TabIndex = 30;
            this.txtPort.Text = "1883";
            // 
            // txtBroker
            // 
            this.txtBroker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBroker.Location = new System.Drawing.Point(15, 23);
            this.txtBroker.Multiline = true;
            this.txtBroker.Name = "txtBroker";
            this.txtBroker.Size = new System.Drawing.Size(199, 30);
            this.txtBroker.TabIndex = 29;
            this.txtBroker.Text = "192.168.80.123";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStart.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnStart.Location = new System.Drawing.Point(404, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 45);
            this.btnStart.TabIndex = 28;
            this.btnStart.Text = "Connect";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-104, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Broker";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1541, 786);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Publisher";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lstPub);
            this.panel4.Controls.Add(this.labelPubMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 102);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1535, 681);
            this.panel4.TabIndex = 7;
            // 
            // lstPub
            // 
            this.lstPub.BackColor = System.Drawing.Color.PaleGreen;
            this.lstPub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPub.FormattingEnabled = true;
            this.lstPub.HorizontalScrollbar = true;
            this.lstPub.ItemHeight = 16;
            this.lstPub.Location = new System.Drawing.Point(0, 20);
            this.lstPub.Name = "lstPub";
            this.lstPub.ScrollAlwaysVisible = true;
            this.lstPub.Size = new System.Drawing.Size(1535, 661);
            this.lstPub.TabIndex = 7;
            // 
            // labelPubMessage
            // 
            this.labelPubMessage.AutoSize = true;
            this.labelPubMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPubMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelPubMessage.Location = new System.Drawing.Point(0, 0);
            this.labelPubMessage.Name = "labelPubMessage";
            this.labelPubMessage.Size = new System.Drawing.Size(136, 20);
            this.labelPubMessage.TabIndex = 6;
            this.labelPubMessage.Text = "Message Data:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gold;
            this.panel3.Controls.Add(this.PausePublisher);
            this.panel3.Controls.Add(this.btnClearPub);
            this.panel3.Controls.Add(this.brokeradress);
            this.panel3.Controls.Add(this.broker);
            this.panel3.Controls.Add(this.labelPubTopic);
            this.panel3.Controls.Add(this.txtPubTopic);
            this.panel3.Controls.Add(this.btnPublish);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1535, 99);
            this.panel3.TabIndex = 6;
            // 
            // PausePublisher
            // 
            this.PausePublisher.AutoSize = true;
            this.PausePublisher.Location = new System.Drawing.Point(1299, 51);
            this.PausePublisher.Name = "PausePublisher";
            this.PausePublisher.Size = new System.Drawing.Size(181, 21);
            this.PausePublisher.TabIndex = 35;
            this.PausePublisher.Text = "PauseResumePublisher";
            this.PausePublisher.UseVisualStyleBackColor = true;
            // 
            // btnClearPub
            // 
            this.btnClearPub.BackColor = System.Drawing.Color.Red;
            this.btnClearPub.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearPub.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClearPub.Location = new System.Drawing.Point(1098, 36);
            this.btnClearPub.Name = "btnClearPub";
            this.btnClearPub.Size = new System.Drawing.Size(130, 45);
            this.btnClearPub.TabIndex = 34;
            this.btnClearPub.Text = "Clear";
            this.btnClearPub.UseVisualStyleBackColor = false;
            this.btnClearPub.Click += new System.EventHandler(this.btnClearPub_Click_1);
            // 
            // brokeradress
            // 
            this.brokeradress.AutoSize = true;
            this.brokeradress.Location = new System.Drawing.Point(801, 25);
            this.brokeradress.Name = "brokeradress";
            this.brokeradress.Size = new System.Drawing.Size(58, 17);
            this.brokeradress.TabIndex = 33;
            this.brokeradress.Text = "Enter IP";
            // 
            // broker
            // 
            this.broker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.broker.Location = new System.Drawing.Point(652, 51);
            this.broker.Multiline = true;
            this.broker.Name = "broker";
            this.broker.Size = new System.Drawing.Size(400, 30);
            this.broker.TabIndex = 32;
            // 
            // labelPubTopic
            // 
            this.labelPubTopic.AutoSize = true;
            this.labelPubTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelPubTopic.Location = new System.Drawing.Point(9, 23);
            this.labelPubTopic.Name = "labelPubTopic";
            this.labelPubTopic.Size = new System.Drawing.Size(129, 20);
            this.labelPubTopic.TabIndex = 29;
            this.labelPubTopic.Text = "Publish Topic:";
            // 
            // txtPubTopic
            // 
            this.txtPubTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPubTopic.Location = new System.Drawing.Point(9, 48);
            this.txtPubTopic.Multiline = true;
            this.txtPubTopic.Name = "txtPubTopic";
            this.txtPubTopic.Size = new System.Drawing.Size(400, 30);
            this.txtPubTopic.TabIndex = 30;
            // 
            // btnPublish
            // 
            this.btnPublish.BackColor = System.Drawing.Color.Green;
            this.btnPublish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPublish.ForeColor = System.Drawing.Color.White;
            this.btnPublish.Location = new System.Drawing.Point(458, 41);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(150, 40);
            this.btnPublish.TabIndex = 31;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = false;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1549, 815);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PSL MQTT Test Form";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtPubMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label lblThreadCount;
        private System.Windows.Forms.Label conlbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPublishTopic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubscribeTopic;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtBroker;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox PauseSubscriber;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lstPub;
        private System.Windows.Forms.Label labelPubMessage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox PausePublisher;
        private System.Windows.Forms.Button btnClearPub;
        private System.Windows.Forms.Label brokeradress;
        private System.Windows.Forms.TextBox broker;
        private System.Windows.Forms.Label labelPubTopic;
        private System.Windows.Forms.TextBox txtPubTopic;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.TextBox txtSearchSubscriber;
        private System.Windows.Forms.Button btnResetSearch;
        private System.Windows.Forms.Button btnSearchSubscriber;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label Search;
    }
}

