using System;
using System.Diagnostics;
using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.rrrControl
{
    /// <summary>
    /// This response is returned for the device ID request command (A0.21). 
    /// Eight bytes of data will be returned, corresponding to the device ID 
    /// previously set using the set device ID command (A8.20). If no device 
    /// ID had been previously set, the video disk recorder will return eight
    ///  null bytes with a value of 0 for the device ID.
    /// </summary>
    public class DeviceID : CommandBlock
    {
        public DeviceID(byte[] data)
        {
            Debug.Assert(data != null);
            if (data.Length != 8)
                throw new ArgumentException("data must be exactly 8 bytes long.");

            Cmd1 = Cmd1.rrrReturn;
            Cmd2 = (byte) rrrReturn.DeviceID;
            Data = data;
        }
    }
}
