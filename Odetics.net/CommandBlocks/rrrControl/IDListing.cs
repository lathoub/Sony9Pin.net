using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.rrrControl
{
    /// <summary>
    /// </summary>
    public class IDListing : CommandBlock
    {
        public IDListing()
        {
            Cmd1 = Cmd1.rrrReturn;
            Cmd2 = (byte)rrrReturn.IDListing;
        }
    }
}
