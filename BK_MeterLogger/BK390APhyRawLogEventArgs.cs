using System;

namespace BK_MeterLogger
{
    public class BK390APhyRawLogEventArgs : EventArgs
    {
        public string PhyFrame { get; private set; }

        public BK390APhyRawLogEventArgs(string frame)
        {
            PhyFrame = frame;
        }
    }
}
