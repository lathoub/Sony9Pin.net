namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiClogTimerEnable : CommandBlock
    {
        /// <summary>
        ///     Enables anti-clog timer.
        /// </summary>
        public AntiClogTimerEnable()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.AntiClogTimerEnable;
        }
    }
}