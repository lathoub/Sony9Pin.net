using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
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