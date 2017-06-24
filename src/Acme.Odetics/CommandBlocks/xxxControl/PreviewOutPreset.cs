using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
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