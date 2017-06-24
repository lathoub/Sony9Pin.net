using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.rrrControl
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
