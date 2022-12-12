using System.Diagnostics;
using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class EraseID : CommandBlock
    {
        /// <summary>
        /// 
        /// </summary>
        public EraseID()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.EraseID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public EraseID(byte[] id)
        {
            Debug.Assert(id != null);
            if (id.Length != 8)
                throw new ArgumentException("id needs to be exactly 8 bytes long.");

            Cmd1DataCount = ToCmd1DataCount(Cmd1.xxxRequest, id.Length);
            Cmd2 = (byte)xxxRequest.EraseID;
            Data = id;
        }
    }
}