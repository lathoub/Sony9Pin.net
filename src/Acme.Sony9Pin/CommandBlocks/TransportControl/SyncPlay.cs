namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class SyncPlay : CommandBlock
    {
        /// <summary>
        ///     Prerolls the _slave for the preset Preroll time, then enters Play mode.
        /// </summary>
        public SyncPlay()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.SyncPlay;
        }
    }
}
