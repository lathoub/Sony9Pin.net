using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.TapeControl
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
