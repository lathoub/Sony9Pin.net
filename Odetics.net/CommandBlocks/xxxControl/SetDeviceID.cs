using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class SetDeviceID : CommandBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public SetDeviceID(byte[] id)
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.SetDeviceID;
        }
    }
}