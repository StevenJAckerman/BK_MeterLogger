using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// Winforms application to display and log RS-232 output from B&K Precision BK-390A multimeter
/// </summary>
namespace BK_MeterLogger
{
    public partial class MainForm : Form
    {
        #region local variables
        private string _commPort;
        private int _baudRate;
        private BK390APhy _phy;
        private readonly ListBoxLog _phyRawListBoxLog;
        private BK390AMeasurementEventArgs _measurementEventArgs;
        private bool _logging;
        private bool _lastLogging;
        private string _logFilename;
        private DateTime _lastDateTime;
        #endregion

        #region constructor
        public MainForm()
        {
            InitializeComponent();

            //
            // http://www.sgrottel.de/?p=1581&lang=en
            //
            Font = SystemFonts.DefaultFont;

            _phyRawListBoxLog = new ListBoxLog(doubleBufferedListBoxPhyLog, "{3} {8}");
        }
        #endregion

        private void commandButtonsEnable(bool enable)
        {

        }

        #region event handlers

        /// <summary>
        /// Comm button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCommPort_Click(object sender, EventArgs e)
        {
            EditCommPortDialog commPortDialog = new EditCommPortDialog {StartPosition = FormStartPosition.Manual};

            do
            {
                _phy?.Dispose();

                if (commPortDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _commPort = commPortDialog.CommPort;
                    _baudRate = commPortDialog.BaudRate;

                    try
                    {
                        commandButtonsEnable(false);

                        _phy = new BK390APhy(_commPort, _baudRate);

                        commandButtonsEnable(true);

                        _phy.PhyRawLogEvent += _phy_PhyRawLogEvent;
                        _phy.MeasurementReceivedEvent += _phy_MeasurementReceivedEvent;
                    }
                    catch (Exception ex)
                    {
                        _phy?.Dispose();
                    }
                    finally
                    {
                        _phy?.Open();
                    }

                    buttonCommPort.Text = _commPort;

                    break;
                }
                else
                {
                    break;
                }

            } while (true);
        }

        /// <summary>
        /// Start/Stop Log button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLog_Click(object sender, EventArgs e)
        {
            if (!_logging)
            {
                _lastDateTime = DateTime.Now;
                _lastLogging = _logging;
                _logging = true;
                buttonLog.Text = "Stop Log";
                comboBoxInterval.Enabled = false;
            }
            else
            {
                _lastLogging = _logging;
                _logging = false;
                buttonLog.Text = "Start Log";
                comboBoxInterval.Enabled = true;
            }
        }
        
        /// <summary>
        /// Show measurment type, value, units on UI, log values
        /// </summary>
        private void _showTypeAndLogMeasurements()
        {
            if (this.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(_showTypeAndLogMeasurements));
            }
            else
            {
                checkBoxAuto.SetChecked(_measurementEventArgs.Auto);

                checkBoxVoltage.SetChecked(false);
                checkBoxOhms.SetChecked(false);
                checkBoxFrequency.SetChecked(false);
                checkBoxCapacitance.SetChecked(false);
                checkBox_uA.SetChecked(false);
                checkBox_mA.SetChecked(false);
                checkBox_20A.SetChecked(false);
                checkBoxTemperature.SetChecked(false);

                switch (_measurementEventArgs.Reading)
                {
                    case BK390AMeasurementEventArgs.ReadingType.Voltage:
                        checkBoxVoltage.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.Ohm:
                        checkBoxOhms.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.FrequencyRpm:
                        checkBoxFrequency.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.Capacitance:
                        checkBoxCapacitance.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.uA_Current:
                        checkBox_uA.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.mA_Current:
                        checkBox_mA.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.A_Current:
                        checkBox_20A.SetChecked(true);
                        break;
                    case BK390AMeasurementEventArgs.ReadingType.Temperature:
                        checkBoxTemperature.SetChecked(true);
                        break;
                }

                if (_measurementEventArgs.OverFlow)
                {
                    textBoxValue.Text = "O.L";
                }
                else
                {
                    textBoxValue.Text = _measurementEventArgs.Value.ToString();
                }

                labelUnits.Text = _measurementEventArgs.Units;

                if (_logging)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan interval = now - _lastDateTime;

                    // Q - first measurement since logging started ?
                    if (_lastLogging == false)
                    {
                        // yes - make first log entry
                        logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                        _lastLogging = _logging;
                    }

                    switch (comboBoxInterval.SelectedIndex)
                    {
                        case 0:
                            logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                            break;

                        case 1:
                            if ((long)interval.TotalSeconds >= 1)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 2:
                            if ((long)interval.TotalSeconds >= 5)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 3:
                            if ((long)interval.TotalSeconds >= 10)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 4:
                            if ((long)interval.TotalSeconds >= 30)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 5:
                            if ((long)interval.TotalMinutes >= 1)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 6:
                            if ((long)interval.TotalMinutes >= 5)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 7:
                            if ((long)interval.TotalMinutes >= 10)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 8:
                            if ((long)interval.TotalMinutes >= 30)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 9:
                            if ((long)interval.TotalHours >= 1)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 10:
                            if ((long)interval.TotalHours >= 5)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 11:
                            if ((long)interval.TotalHours >= 10)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                        case 12:
                            if (interval.TotalDays >= 1)
                            {
                                logMeasurement(now, textBoxValue.Text, labelUnits.Text);
                                _lastDateTime = now;
                            }
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// Append measurement to log file
        /// </summary>
        /// <param name="when"></param>
        /// <param name="value"></param>
        /// <param name="units"></param>
        private void logMeasurement(DateTime when, string value, string units)
        {
            using (StreamWriter sw = File.AppendText(_logFilename))
            {
                sw.WriteLine("{0},{1},{2}", when.ToString((comboBoxInterval.SelectedIndex == 0) ? "HH:mm:ss.fff" : "HH:mm:ss"), value, units);
            }
        }

        /// <summary>
        /// Measurement received from BK390A event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _phy_MeasurementReceivedEvent(object sender, BK390AMeasurementEventArgs e)
        {
            _measurementEventArgs = e;
            _showTypeAndLogMeasurements();
        }

        /// <summary>
        /// BK390 raw packet event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _phy_PhyRawLogEvent(object sender, BK390APhyRawLogEventArgs e)
        {
            _phyRawListBoxLog.Log(Level.Info, e.PhyFrame);
        }

        /// <summary>
        /// MainForm loading event hanlder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.MainFormPosition != null)
            {
                Properties.Settings.Default.MainFormPosition.Restore(this);
            }

            _logFilename = Properties.Settings.Default.LastLogFile;
            comboBoxInterval.SelectedIndex = Properties.Settings.Default.LastInterval;
            
            _commPort = Properties.Settings.Default.LastCommPort;

            _baudRate = 2400;
            try
            {
                _baudRate = int.Parse(Properties.Settings.Default.LastBaudRate);
            }
            catch
            {
                ;
            }

            if (_commPort.Length > 0
                && _commPort.Contains("COM")
                && (_baudRate >= 1200
                    && _baudRate <= 57600)
                && (SerialPort.GetPortNames().Any(x => x == _commPort)))
            {
                try
                {
                    commandButtonsEnable(false);

                    _phy = new BK390APhy(_commPort, _baudRate);

                    commandButtonsEnable(true);

                    _phy.PhyRawLogEvent += _phy_PhyRawLogEvent;
                    _phy.MeasurementReceivedEvent += _phy_MeasurementReceivedEvent;
                }
                catch
                {
                    _phy?.Dispose();

                    BeginInvoke((MethodInvoker) delegate { buttonCommPort_Click(this, e); });
                }
                finally
                {
                    _phy?.Open();
                }

                buttonCommPort.Text = _commPort;
            }
            else
            {
                BeginInvoke((MethodInvoker) delegate { buttonCommPort_Click(this, e); });
            }
        }

        /// <summary>
        /// MainForm closing event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.MainFormPosition == null)
            {
                Properties.Settings.Default.MainFormPosition = new WindowSettings();
            }

            Properties.Settings.Default.MainFormPosition.Record(this);
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Menustrip Exit item click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// generates a shortened path with ellipsis
        /// 
        /// http://chadkuehn.com/shrink-file-paths-with-an-ellipsis-in-c/
        /// 
        /// </summary>
        /// <param name="absolutepath">The path to compress</param>
        /// <param name="limit">The maximum length</param>
        /// <param name="delimiter">The character(s) to use to imply incompleteness</param>
        /// <returns></returns>
        private string shrinkPath(string absolutepath, int limit, string delimiter = "…")
        {
            //no path provided
            if (string.IsNullOrEmpty(absolutepath))
            {
                return "";
            }

            var name = Path.GetFileName(absolutepath);
            int namelen = name.Length;
            int pathlen = absolutepath.Length;
            var dir = absolutepath.Substring(0, pathlen - namelen);

            int delimlen = delimiter.Length;
            int idealminlen = namelen + delimlen;

            var slash = (absolutepath.IndexOf("/") > -1 ? "/" : "\\");

            //less than the minimum amt
            if (limit < ((2 * delimlen) + 1))
            {
                return "";
            }

            //fullpath
            if (limit >= pathlen)
            {
                return absolutepath;
            }

            //file name condensing
            if (limit < idealminlen)
            {
                return delimiter + name.Substring(0, (limit - (2 * delimlen))) + delimiter;
            }

            //whole name only, no folder structure shown
            if (limit == idealminlen)
            {
                return delimiter + name;
            }

            return dir.Substring(0, (limit - (idealminlen + 1))) + delimiter + slash + name;
        }

        /// <summary>
        /// Menustrip New item event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select New Log File";
            openFileDialog.CheckFileExists = false;

            if (_logging)
            {
                buttonLog_Click(this, EventArgs.Empty);
                buttonLog.Enabled = false;
                comboBoxInterval.Enabled = false;
            }

            openFileDialog.FileName = _logFilename;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _logFilename = openFileDialog.FileName;

                if (File.Exists(_logFilename))
                {
                    StringBuilder sb = new StringBuilder(shrinkPath(_logFilename, 30));
                    sb.Append(" exists - Overwrite ?");
                    switch (MessageBox.Show(sb.ToString(), "File Exists", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning))
                    {
                        case DialogResult.No:
                            break;

                        case DialogResult.Yes:
                            File.Delete(_logFilename);
                            buttonLog.Enabled = true;
                            comboBoxInterval.Enabled = true;
                            Properties.Settings.Default.LastLogFile = _logFilename;
                            break;
                    }
                }
                else
                {
                    buttonLog.Enabled = true;
                    comboBoxInterval.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Menustrip Open item event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select Existing Log File";
            openFileDialog.CheckFileExists = true;

            if (_logging)
            {
                buttonLog_Click(this, EventArgs.Empty);
                buttonLog.Enabled = false;
                comboBoxInterval.Enabled = false;
            }

            openFileDialog.FileName = _logFilename;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _logFilename = openFileDialog.FileName;

                buttonLog.Enabled = true;
                comboBoxInterval.Enabled = true;
                Properties.Settings.Default.LastLogFile = _logFilename;
            }
        }

        /// <summary>
        /// Log interval drop list box selected index changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastInterval = comboBoxInterval.SelectedIndex;
        }

        #endregion
    }
}
