using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class PreviewOutReset : CommandBlock
    {
        public PreviewOutReset()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.PreviewOutReset;
        }
    }
}