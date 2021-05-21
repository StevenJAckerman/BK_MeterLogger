using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

/// <summary>
/// BK390A interface physical layer
/// </summary>
namespace BK_MeterLogger
{
    public class BK390APhy : IDisposable
    {
        #region Constants

        private static readonly ushort[] BaudRateValues = {2400};

        private const byte CR = 0x0d;
        private const byte LF = 0x0a;

        private const int MaxFrameLength = 9;

        enum FrameState
        {
            SyncCR,
            SyncLF,
            Accumulate,
            Complete
        };

        #endregion

        #region Local Variables

        private readonly SerialPort _port;
        private byte[] _receivedFrame;
        private string _receivedPhy;
        private readonly object _receivedDataLock = new object();
        private FrameState _frameState;

        #endregion

        #region Properties

        public string[] CommPortNames
        {
            get { return SerialPort.GetPortNames(); }
        }

        public ushort[] BaudRates
        {
            get { return BaudRateValues; }
        }

        public bool IsOpen
        {
            get { return _port.IsOpen; }
        }

        #endregion

        #region Constructors and Destructors

        public BK390APhy(string commPort, int baudRate)
        {
            _port = new SerialPort(commPort, baudRate, Parity.Odd, 7, StopBits.One);

            _port.Handshake = Handshake.None;

            _port.Encoding = Encoding.GetEncoding("Windows-1252");

            _receivedPhy = "";
            _receivedFrame = new byte[0];
            _port.DataReceived += _port_DataReceived;
            _frameState = FrameState.SyncCR;
        }

        #endregion
        
        #region EventHandlers & Delegates

        protected virtual void OnPhyLogEvent(string phyFrame)
        {
            PhyRawLogEvent?.Invoke(this, new BK390APhyRawLogEventArgs(phyFrame));
        }

        protected virtual void OnMeasurementReceivedEvent(byte[] frame)
        {
            MeasurementReceivedEvent?.Invoke(this, new BK390AMeasurementEventArgs(frame));
        }

        public event EventHandler<BK390APhyRawLogEventArgs> PhyRawLogEvent;
        public event EventHandler<BK390AMeasurementEventArgs> MeasurementReceivedEvent;

        /// <summary>
        /// Serial port data received event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int frameNdx = _receivedFrame.Length;
            StringBuilder sb = new StringBuilder();

            while (_port.IsOpen && _port.BytesToRead != 0)
            {
                int bytesToRead = _port.BytesToRead;
                byte[] receivedData = new byte[bytesToRead];
                bytesToRead = _port.Read(receivedData, 0, bytesToRead);

                int dataNdx = 0;

                Monitor.Enter(_receivedDataLock);

                while (dataNdx < bytesToRead)
                {
                    switch (_frameState)
                    {
                        case FrameState.SyncCR:
                            switch (receivedData[dataNdx])
                            {
                                case CR:
                                    _frameState = FrameState.SyncLF;
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case FrameState.SyncLF:
                            switch (receivedData[dataNdx])
                            {
                                case LF:
                                    if (frameNdx == MaxFrameLength)
                                    {
                                        _receivedPhy += sb.ToString();
                                        OnPhyLogEvent(_receivedPhy);

                                        OnMeasurementReceivedEvent(_receivedFrame);
                                    }

                                    _receivedFrame = new byte[0];
                                    frameNdx = 0;
                                    _receivedPhy = "";
                                    sb.Length = 0;
                                    _frameState = FrameState.Accumulate;
                                    break;

                                default:
                                    _frameState = FrameState.SyncCR;
                                    break;
                            }
                            break;

                        case FrameState.Accumulate:
                            switch (receivedData[dataNdx])
                            {
                                case CR:
                                    _frameState = FrameState.SyncLF;
                                    break;

                                case LF:
                                    _frameState = FrameState.SyncCR;
                                    break;

                                default:
                                    if (frameNdx < MaxFrameLength)
                                    {
                                        Array.Resize(ref _receivedFrame, frameNdx + 1);
                                        _receivedFrame[frameNdx++] = receivedData[dataNdx];
                                    }
                                    else
                                    {
                                        _receivedFrame = _receivedFrame.Skip(1).Concat(new byte[] {0}).ToArray();
                                        _receivedFrame[frameNdx-1] = receivedData[dataNdx];
                                    }

                                    sb.AppendFormat("{0:X2} ", receivedData[dataNdx]);
                                    break;
                            }
                            break;
                    }   // switch ()

                    dataNdx++;

                }   // while ()

                Monitor.Exit(_receivedDataLock);

            }   // while ()

            _receivedPhy += sb.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the serial port
        /// </summary>
        public void Open()
        {
            _port?.Open();
        }

        /// <summary>
        /// properly disposes the serial port
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_port != null)
                {
                    if (_port.IsOpen)
                    {
                        _port.DataReceived -= null;
                        _port.Close();
                        Thread.Sleep(100);
                    }
                    _port.Dispose();
                }
            }
        }

        /// <summary>
        /// Dispose the phy
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
