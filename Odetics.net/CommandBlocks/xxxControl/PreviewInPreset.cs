using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class PreviewInPreset : CommandBlock
    {
        public PreviewInPreset()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.PreviewInPreset;
        }
    }
}