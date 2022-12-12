namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Stop : CommandBlock
    {
        /// <summary>
        ///     This command places the Video disk recorder in the Stop mode, and any “motion”
        ///     command such as Play, fast forward, Record, etc., will Stop. This command will immediately
        ///     abort any current auto Play or Record process, but will not reset the auto mode
        ///     presets and Preview presets.
        ///     When a Stop command is issued, the Stop status (status byte 1, bit 5) will be set high,
        ///     and the Rewind status (status byte 1, bit 3), fast forward status (status byte 1, bit 2), Record
        ///     status (status byte 1, bit 1), Play status (status byte 1, bit 0), Shuttle status (status
        ///     byte 2, bit 5), Jog status (status byte 2, bit 4), and variable Play status (status byte 2, bit 3)
        ///     will all be set low.
        /// </summary>
        public Stop()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Stop;
        }
    }
}
