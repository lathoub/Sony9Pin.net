using System;
using System.Diagnostics;
using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.rrrControl
{
    /// <summary>
    /// This response is returned for the Longest Available storage request command (A0.1C). 
    /// Four bytes of data will be returned, representing the amount of the longest contiguous 
    /// available storage in the video disk recorder expressed in hours, minutes, seconds, and frames.
    /// The available storage time will be calculated based on worst case compression requirements. 
    /// The longest available storage time is formatted similar to the 4 byte BCD format for time code
    ///  positions, but the hours digits will be permitted to reach 99 hours. If the available storage
    ///  time exceeds 99:59:59:00, the video disk recorder should return 99:59:59:00.
    /// </summary>
    public class LongestContiguousAvailableStorage : CommandBlock
    {
        public LongestContiguousAvailableStorage(byte[] data)
        {
            Debug.Assert(data != null);
            if (data.Length != 8)
                throw new ArgumentException("data must be exactly 8 bytes long.");

            Cmd1 = Cmd1.rrrReturn;
            Cmd2 = (byte) rrrReturn.LongestContiguousAvailableStorage;
            Data = data;
        }
    }
}
