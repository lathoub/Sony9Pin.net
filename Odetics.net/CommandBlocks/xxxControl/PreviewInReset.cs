using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class PreviewInReset : CommandBlock
    {
        public PreviewInReset()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.PreviewInReset;
        }
    }
}