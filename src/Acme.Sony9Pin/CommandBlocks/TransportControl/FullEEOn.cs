namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class FullEEOn : CommandBlock
    {
        /// <summary>
        ///     Sets all channels to EE mode regardless of EDIT PRESET channels assigned by the 41.30 EDIT PRESET command. It takes
        ///     the _slave 5 frames to perform this operation after it receieves the command.
        /// </summary>
        public FullEEOn()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.FullEEOn;
        }
    }
}