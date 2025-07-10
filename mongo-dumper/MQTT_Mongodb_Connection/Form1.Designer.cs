namespace MQTT_Mongodb_Connection
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
            this.lblSettings = new System.Windows.Forms.Label();
            this.txtMongoConn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMongoDb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMongoCol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.lblConnStatus = new System.Windows.Forms.Label();
            this.btnStartDump = new System.Windows.Forms.Button();
            this.btnStopDump = new System.Windows.Forms.Button();
            this.lblDumpStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewMessages = new System.Windows.Forms.DataGridView();
            this.colTopic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSettings.Location = new System.Drawing.Point(12, 9);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(205, 19);
            this.lblSettings.TabIndex = 0;
            this.lblSettings.Text = "MongoDB Connection Settings";
            // 
            // txtMongoConn
            // 
            this.txtMongoConn.Location = new System.Drawing.Point(170, 40);
            this.txtMongoConn.Name = "txtMongoConn";
            this.txtMongoConn.Size = new System.Drawing.Size(600, 22);
            this.txtMongoConn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection String:";
            // 
            // txtMongoDb
            // 
            this.txtMongoDb.Location = new System.Drawing.Point(170, 70);
            this.txtMongoDb.Name = "txtMongoDb";
            this.txtMongoDb.Size = new System.Drawing.Size(220, 22);
            this.txtMongoDb.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Database Name:";
            // 
            // txtMongoCol
            // 
            this.txtMongoCol.Location = new System.Drawing.Point(170, 100);
            this.txtMongoCol.Name = "txtMongoCol";
            this.txtMongoCol.Size = new System.Drawing.Size(220, 22);
            this.txtMongoCol.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Collection Name:";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(420, 72);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(120, 23);
            this.btnTestConn.TabIndex = 4;
            this.btnTestConn.Text = "Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = true;
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.AutoSize = true;
            this.lblConnStatus.Location = new System.Drawing.Point(560, 76);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(110, 16);
            this.lblConnStatus.TabIndex = 8;
            this.lblConnStatus.Text = "Status: [------]";
            // 
            // btnStartDump
            // 
            this.btnStartDump.Location = new System.Drawing.Point(20, 140);
            this.btnStartDump.Name = "btnStartDump";
            this.btnStartDump.Size = new System.Drawing.Size(120, 27);
            this.btnStartDump.TabIndex = 5;
            this.btnStartDump.Text = "Start Dumping";
            this.btnStartDump.UseVisualStyleBackColor = true;
            // 
            // btnStopDump
            // 
            this.btnStopDump.Location = new System.Drawing.Point(160, 140);
            this.btnStopDump.Name = "btnStopDump";
            this.btnStopDump.Size = new System.Drawing.Size(120, 27);
            this.btnStopDump.TabIndex = 6;
            this.btnStopDump.Text = "Stop Dumping";
            this.btnStopDump.UseVisualStyleBackColor = true;
            // 
            // lblDumpStatus
            // 
            this.lblDumpStatus.AutoSize = true;
            this.lblDumpStatus.Location = new System.Drawing.Point(300, 146);
            this.lblDumpStatus.Name = "lblDumpStatus";
            this.lblDumpStatus.Size = new System.Drawing.Size(128, 16);
            this.lblDumpStatus.TabIndex = 11;
            this.lblDumpStatus.Text = "Dump Status: [---]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "Recent MQTT Messages Saved";
            // 
            // dataGridViewMessages
            // 
            this.dataGridViewMessages.AllowUserToAddRows = false;
            this.dataGridViewMessages.AllowUserToDeleteRows = false;
            this.dataGridViewMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right));
            this.dataGridViewMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTopic,
            this.colTimestamp,
            this.colPayload});
            this.dataGridViewMessages.Location = new System.Drawing.Point(15, 215);
            this.dataGridViewMessages.MultiSelect = false;
            this.dataGridViewMessages.Name = "dataGridViewMessages";
            this.dataGridViewMessages.ReadOnly = true;
            this.dataGridViewMessages.RowTemplate.Height = 25;
            this.dataGridViewMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMessages.Size = new System.Drawing.Size(1485, 230);
            this.dataGridViewMessages.TabIndex = 13;
            // 
            // colTopic
            // 
            this.colTopic.HeaderText = "Topic";
            this.colTopic.Name = "colTopic";
            this.colTopic.ReadOnly = true;
            this.colTopic.Width = 350;
            // 
            // colTimestamp
            // 
            this.colTimestamp.HeaderText = "Timestamp";
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            this.colTimestamp.Width = 200;
            // 
            // colPayload
            // 
            this.colPayload.HeaderText = "Payload Preview";
            this.colPayload.Name = "colPayload";
            this.colPayload.ReadOnly = true;
            this.colPayload.Width = 900;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(1400, 180);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(100, 27);
            this.btnReload.TabIndex = 7;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1517, 486);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dataGridViewMessages);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDumpStatus);
            this.Controls.Add(this.btnStopDump);
            this.Controls.Add(this.btnStartDump);
            this.Controls.Add(this.lblConnStatus);
            this.Controls.Add(this.btnTestConn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMongoCol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMongoDb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMongoConn);
            this.Controls.Add(this.lblSettings);
            this.Name = "Form1";
            this.Text = "MQTT to MongoDB Dump";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.TextBox txtMongoConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMongoDb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMongoCol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Label lblConnStatus;
        private System.Windows.Forms.Button btnStartDump;
        private System.Windows.Forms.Button btnStopDump;
        private System.Windows.Forms.Label lblDumpStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTopic;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayload;
        private System.Windows.Forms.Button btnReload;
    }
}