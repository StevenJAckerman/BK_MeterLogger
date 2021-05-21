using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

/// <summary>
/// Edit dialog to select the serial comm port and baud rate
/// </summary>
namespace BK_MeterLogger
{
	public partial class EditCommPortDialog : Form
	{
		public string CommPort
		{
			get { return comboBoxCommPort.SelectedItem.ToString(); }
			set { comboBoxCommPort.SelectedItem = value; }
		}

		public int BaudRate
		{
			get { return int.Parse(comboBoxBaudRate.SelectedItem.ToString()); }
			set { comboBoxBaudRate.SelectedItem = value.ToString(); }
		}

		public EditCommPortDialog()
		{
			InitializeComponent();

		}

		private void EditCommPortDialog_Load(object sender, EventArgs e)
		{
			List<string> availableCommPorts = new List<string>(SerialPort.GetPortNames());
			comboBoxCommPort.DataSource = availableCommPorts;

            CommPort = Properties.Settings.Default.LastCommPort;

            try { BaudRate = int.Parse(Properties.Settings.Default.LastBaudRate); }
			catch { ;}

            if (Properties.Settings.Default.EditCommPortDialogPosition != null)
			{
				Properties.Settings.Default.EditCommPortDialogPosition.Restore(this);
			}
			else
			{
				this.Location = this.Owner.Location + (this.Owner.Size - this.Size);
			}
		}

		private void EditCommPortDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				Properties.Settings.Default.LastCommPort = CommPort;
				Properties.Settings.Default.LastBaudRate = BaudRate.ToString();
			}

			if (Properties.Settings.Default.EditCommPortDialogPosition == null)
			{
				Properties.Settings.Default.EditCommPortDialogPosition = new WindowSettings();
			}

			Properties.Settings.Default.EditCommPortDialogPosition.Record(this);
			Properties.Settings.Default.Save();
		}
	}
}
