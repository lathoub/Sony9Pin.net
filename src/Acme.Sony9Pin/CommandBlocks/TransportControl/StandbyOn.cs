namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class StandbyOn : CommandBlock
    {
        /// <summary>
        ///     Turns on Standby mode. For VTR, this causes the machine to stay threaded when in Stop. Affects EE/Tape selection.
        /// </summary>
        public StandbyOn()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.StandbyOn;
        }
    }
}
