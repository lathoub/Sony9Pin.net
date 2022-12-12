namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Record : CommandBlock
    {
        /// <summary>
        ///     This command places the Video disk recorder in the Record mode, and recording will
        ///     begin. Recording begins from the current time code position on the currently loaded Id,
        ///     and the time code position will increase and new Video material will be recorded until
        ///     recording is stopped.
        ///     If auto mode is enabled, the Record command will initiate the auto Record processing
        ///     described under the auto mode on command (40.41).
        ///     There should be a fixed latency in number of frames from the time this command is
        ///     issued, after a Record cue is issued, until the Video disk recorder actually starts recording,
        ///     so that the controlling device can frame accurately synchronize the source material.
        ///     When a Record command is issued, the Stop status (status byte 1, bit 5) will be set
        ///     low, and the Record status (status byte 1, bit 1), Play status (status byte 1, bit 0) and
        ///     servo lock status (status byte 2, bit 7) will be set high.
        /// </summary>
        public Record()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Record;
        }
    }
}
