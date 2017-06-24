namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiClogTimerDisable : CommandBlock
    {
        /// <summary>
        ///     Disables the anti-clog timer. This timer is responsible for unthreading the tape upon timeout to save wear on the
        ///     heads. If a system disables this timer, it should take responsibility for head wear avoidance itself.
        /// </summary>
        public AntiClogTimerDisable()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.AntiClogTimerDisable;
        }
    }
}