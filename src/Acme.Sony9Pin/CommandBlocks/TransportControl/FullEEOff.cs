namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class FullEEOff : CommandBlock
    {
        /// <summary>
        ///     Clears all channels from EE mode regardless of EDIT PRESET channels assigned by the 41.30 EDIT PRESET command. It
        ///     takes the _slave 5 frames to perform this operation after it receieves the command.
        /// </summary>
        public FullEEOff()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.FullEEOff;
        }
    }
}