namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectEEOn : CommandBlock
    {
        /// <summary>
        ///     This command sets only the preset channels assigned by the 41.30 Edit Preset command to EE mode. The EE mode is
        ///     cleared by the 20.64 Edit Off command. It takes the _slave 5 frames to perform this operation after receiving the
        ///     command.
        /// </summary>
        public SelectEEOn()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.SelectEEOn;
        }
    }
}