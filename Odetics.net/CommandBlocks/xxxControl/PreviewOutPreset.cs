using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class PreviewOutPreset : CommandBlock
    {
        public PreviewOutPreset()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.PreviewOutPreset;
        }
    }
}