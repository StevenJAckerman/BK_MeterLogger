namespace BK_MeterLogger
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonCommPort = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.doubleBufferedListBoxPhyLog = new BK_MeterLogger.DoubleBufferedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxVoltage = new System.Windows.Forms.CheckBox();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            this.checkBoxOhms = new System.Windows.Forms.CheckBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.checkBoxFrequency = new System.Windows.Forms.CheckBox();
            this.labelUnits = new System.Windows.Forms.Label();
            this.checkBoxCapacitance = new System.Windows.Forms.CheckBox();
            this.checkBox_uA = new System.Windows.Forms.CheckBox();
            this.checkBoxTemperature = new System.Windows.Forms.CheckBox();
            this.checkBox_mA = new System.Windows.Forms.CheckBox();
            this.checkBox_20A = new System.Windows.Forms.CheckBox();
            this.comboBoxInterval = new System.Windows.Forms.ComboBox();
            this.buttonLog = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCommPort
            // 
            this.buttonCommPort.Location = new System.Drawing.Point(505, 23);
            this.buttonCommPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCommPort.Name = "buttonCommPort";
            this.buttonCommPort.Size = new System.Drawing.Size(105, 28);
            this.buttonCommPort.TabIndex = 0;
            this.buttonCommPort.Text = "Comm";
            this.buttonCommPort.UseVisualStyleBackColor = true;
            this.buttonCommPort.Click += new System.EventHandler(this.buttonCommPort_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.doubleBufferedListBoxPhyLog);
            this.groupBox1.Controls.Add(this.buttonCommPort);
            this.groupBox1.Location = new System.Drawing.Point(16, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(636, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BK390A Data";
            // 
            // doubleBufferedListBoxPhyLog
            // 
            this.doubleBufferedListBoxPhyLog.FormattingEnabled = true;
            this.doubleBufferedListBoxPhyLog.ItemHeight = 20;
            this.doubleBufferedListBoxPhyLog.Location = new System.Drawing.Point(8, 23);
            this.doubleBufferedListBoxPhyLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.doubleBufferedListBoxPhyLog.Name = "doubleBufferedListBoxPhyLog";
            this.doubleBufferedListBoxPhyLog.Size = new System.Drawing.Size(488, 84);
            this.doubleBufferedListBoxPhyLog.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.comboBoxInterval);
            this.groupBox2.Controls.Add(this.buttonLog);
            this.groupBox2.Location = new System.Drawing.Point(16, 191);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(636, 180);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Measurement";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxVoltage);
            this.groupBox3.Controls.Add(this.checkBoxAuto);
            this.groupBox3.Controls.Add(this.checkBoxOhms);
            this.groupBox3.Controls.Add(this.textBoxValue);
            this.groupBox3.Controls.Add(this.checkBoxFrequency);
            this.groupBox3.Controls.Add(this.labelUnits);
            this.groupBox3.Controls.Add(this.checkBoxCapacitance);
            this.groupBox3.Controls.Add(this.checkBox_uA);
            this.groupBox3.Controls.Add(this.checkBoxTemperature);
            this.groupBox3.Controls.Add(this.checkBox_mA);
            this.groupBox3.Controls.Add(this.checkBox_20A);
            this.groupBox3.Location = new System.Drawing.Point(127, 23);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(501, 145);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // checkBoxVoltage
            // 
            this.checkBoxVoltage.AutoCheck = false;
            this.checkBoxVoltage.AutoSize = true;
            this.checkBoxVoltage.Location = new System.Drawing.Point(8, 23);
            this.checkBoxVoltage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxVoltage.Name = "checkBoxVoltage";
            this.checkBoxVoltage.Size = new System.Drawing.Size(78, 21);
            this.checkBoxVoltage.TabIndex = 0;
            this.checkBoxVoltage.Text = "Voltage";
            this.checkBoxVoltage.UseVisualStyleBackColor = true;
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.AutoCheck = false;
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(275, 23);
            this.checkBoxAuto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(59, 21);
            this.checkBoxAuto.TabIndex = 5;
            this.checkBoxAuto.Text = "Auto";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            // 
            // checkBoxOhms
            // 
            this.checkBoxOhms.AutoCheck = false;
            this.checkBoxOhms.AutoSize = true;
            this.checkBoxOhms.Location = new System.Drawing.Point(8, 52);
            this.checkBoxOhms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxOhms.Name = "checkBoxOhms";
            this.checkBoxOhms.Size = new System.Drawing.Size(67, 21);
            this.checkBoxOhms.TabIndex = 1;
            this.checkBoxOhms.Text = "Ohms";
            this.checkBoxOhms.UseVisualStyleBackColor = true;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValue.Location = new System.Drawing.Point(241, 55);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(128, 46);
            this.textBoxValue.TabIndex = 5;
            this.textBoxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxFrequency
            // 
            this.checkBoxFrequency.AutoCheck = false;
            this.checkBoxFrequency.AutoSize = true;
            this.checkBoxFrequency.Location = new System.Drawing.Point(8, 81);
            this.checkBoxFrequency.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxFrequency.Name = "checkBoxFrequency";
            this.checkBoxFrequency.Size = new System.Drawing.Size(97, 21);
            this.checkBoxFrequency.TabIndex = 4;
            this.checkBoxFrequency.Text = "Frequency";
            this.checkBoxFrequency.UseVisualStyleBackColor = true;
            // 
            // labelUnits
            // 
            this.labelUnits.AutoSize = true;
            this.labelUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnits.Location = new System.Drawing.Point(371, 59);
            this.labelUnits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUnits.Name = "labelUnits";
            this.labelUnits.Size = new System.Drawing.Size(37, 39);
            this.labelUnits.TabIndex = 6;
            this.labelUnits.Text = "_";
            this.labelUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxCapacitance
            // 
            this.checkBoxCapacitance.AutoCheck = false;
            this.checkBoxCapacitance.AutoSize = true;
            this.checkBoxCapacitance.Location = new System.Drawing.Point(8, 110);
            this.checkBoxCapacitance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxCapacitance.Name = "checkBoxCapacitance";
            this.checkBoxCapacitance.Size = new System.Drawing.Size(108, 21);
            this.checkBoxCapacitance.TabIndex = 4;
            this.checkBoxCapacitance.Text = "Capacitance";
            this.checkBoxCapacitance.UseVisualStyleBackColor = true;
            // 
            // checkBox_uA
            // 
            this.checkBox_uA.AutoCheck = false;
            this.checkBox_uA.AutoSize = true;
            this.checkBox_uA.Location = new System.Drawing.Point(125, 23);
            this.checkBox_uA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox_uA.Name = "checkBox_uA";
            this.checkBox_uA.Size = new System.Drawing.Size(47, 21);
            this.checkBox_uA.TabIndex = 4;
            this.checkBox_uA.Text = "uA";
            this.checkBox_uA.UseVisualStyleBackColor = true;
            // 
            // checkBoxTemperature
            // 
            this.checkBoxTemperature.AutoCheck = false;
            this.checkBoxTemperature.AutoSize = true;
            this.checkBoxTemperature.Location = new System.Drawing.Point(125, 110);
            this.checkBoxTemperature.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxTemperature.Name = "checkBoxTemperature";
            this.checkBoxTemperature.Size = new System.Drawing.Size(112, 21);
            this.checkBoxTemperature.TabIndex = 4;
            this.checkBoxTemperature.Text = "Temperature";
            this.checkBoxTemperature.UseVisualStyleBackColor = true;
            // 
            // checkBox_mA
            // 
            this.checkBox_mA.AutoCheck = false;
            this.checkBox_mA.AutoSize = true;
            this.checkBox_mA.Location = new System.Drawing.Point(125, 50);
            this.checkBox_mA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox_mA.Name = "checkBox_mA";
            this.checkBox_mA.Size = new System.Drawing.Size(50, 21);
            this.checkBox_mA.TabIndex = 4;
            this.checkBox_mA.Text = "mA";
            this.checkBox_mA.UseVisualStyleBackColor = true;
            // 
            // checkBox_20A
            // 
            this.checkBox_20A.AutoCheck = false;
            this.checkBox_20A.AutoSize = true;
            this.checkBox_20A.Location = new System.Drawing.Point(125, 81);
            this.checkBox_20A.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox_20A.Name = "checkBox_20A";
            this.checkBox_20A.Size = new System.Drawing.Size(55, 21);
            this.checkBox_20A.TabIndex = 4;
            this.checkBox_20A.Text = "20A";
            this.checkBox_20A.UseVisualStyleBackColor = true;
            // 
            // comboBoxInterval
            // 
            this.comboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterval.FormattingEnabled = true;
            this.comboBoxInterval.Items.AddRange(new object[] {
            "~0.5 Sec",
            "~1 Sec",
            "~5 Secs",
            "~10 Secs",
            "~30 Secs",
            "~1 Min",
            "~5 Mins",
            "~10 Mins",
            "~30 Mins",
            "~1 Hour",
            "~5 Hours",
            "~10 Hours",
            "~1 Day"});
            this.comboBoxInterval.Location = new System.Drawing.Point(8, 105);
            this.comboBoxInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxInterval.Name = "comboBoxInterval";
            this.comboBoxInterval.Size = new System.Drawing.Size(109, 24);
            this.comboBoxInterval.TabIndex = 8;
            this.comboBoxInterval.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterval_SelectedIndexChanged);
            // 
            // buttonLog
            // 
            this.buttonLog.Enabled = false;
            this.buttonLog.Location = new System.Drawing.Point(8, 48);
            this.buttonLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(111, 46);
            this.buttonLog.TabIndex = 7;
            this.buttonLog.Text = "Start Log";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(668, 28);
            this.menuStrip.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "B & K 390A Meter Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCommPort;
        private DoubleBufferedListBox doubleBufferedListBoxPhyLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxVoltage;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.CheckBox checkBoxTemperature;
        private System.Windows.Forms.CheckBox checkBox_20A;
        private System.Windows.Forms.CheckBox checkBox_mA;
        private System.Windows.Forms.CheckBox checkBox_uA;
        private System.Windows.Forms.CheckBox checkBoxCapacitance;
        private System.Windows.Forms.CheckBox checkBoxFrequency;
        private System.Windows.Forms.CheckBox checkBoxOhms;
        private System.Windows.Forms.Label labelUnits;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button buttonLog;
        private System.Windows.Forms.CheckBox checkBoxAuto;
        private System.Windows.Forms.ComboBox comboBoxInterval;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

