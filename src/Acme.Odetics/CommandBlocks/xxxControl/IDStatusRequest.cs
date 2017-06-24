using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
{
    public class IDStatusRequest : CommandBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public IDStatusRequest(byte[] id)
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.IDStatusRequest;
        }
    }
}