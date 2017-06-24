namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Play : CommandBlock
    {
        /// <summary>
        ///     This command places the Video disk recorder in the Play mode, and playback will
        ///     begin. Playback begins from the current time code position on the currently loaded Id,
        ///     and the time code position will increase and new Video material will be output until
        ///     playback is stopped.
        ///     If auto mode is enabled, the Play command will initiate the auto Play processing described
        ///     under the auto mode on command (40.41).
        ///     There should be a fixed latency in number of frames from the time this command is
        ///     issued, after a Play cue or in preset is issued, until the Video disk recorder actually starts
        ///     playback, so that the controlling device can frame accurately synchronize the source
        ///     material.
        ///     When a Play command is issued, the Stop status (status byte 1, bit 5) will be set low,
        ///     and the Play status (status byte 1, bit 0) and servo lock status (status byte 2, bit 7) will be
        ///     set high.
        /// </summary>
        public Play()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Play;
        }
    }
}
