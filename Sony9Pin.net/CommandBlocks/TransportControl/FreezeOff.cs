namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class FreezeOff : CommandBlock
    {
        /// <summary>
        ///     This command un-freezes the output of the device.
        /// </summary>
        public FreezeOff()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.FreezeOff;
        }
    }
}