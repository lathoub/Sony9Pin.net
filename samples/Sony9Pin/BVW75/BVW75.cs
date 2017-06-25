using System;

using Acme.Sony9Pin;
using Acme.Sony9Pin.CommandBlocks;
using Acme.Sony9Pin.CommandBlocks.TimeSenseRequest;

namespace BVW75
{
    public class BVW75 : Sony9PinSlave
    {
        /// <summary>
        /// </summary>
        protected BVW75()
        {
            DeviceType = new byte[] {0x00, 0x00};
        }

        protected override CommandBlock CurrentTimeSense(TimeSenseRequest timeSenseRequest)
        {
            throw new NotImplementedException();
        }

        protected override CommandBlock StatusSense(int start, int length)
        {
            throw new NotImplementedException();
        }

        protected override CommandBlock TcGenSense(TimeSenseRequest timeSenseRequest)
        {
            throw new NotImplementedException();
        }
    }
}
