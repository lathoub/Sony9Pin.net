using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.TapeControl
{
    /// <summary>
    /// </summary>
    public class FormatTape : CommandBlock
    {
        public FormatTape()
        {
            Cmd1 = Cmd1.TapeControl;
            Cmd2 = (byte) TapeControl.FormatTape;
        }
    }
}
