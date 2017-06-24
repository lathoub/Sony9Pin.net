using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
{
    public class ListFirstID : CommandBlock
    {
        public ListFirstID()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.ListFirstID;
        }
    }
}