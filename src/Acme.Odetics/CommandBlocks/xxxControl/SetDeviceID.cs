using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
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