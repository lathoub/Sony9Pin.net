namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgSpeedPlayPlus : CommandBlock
    {
        /// <summary>
        ///     This command Play back the _slave device in steps of 0.1% within the range of +/- 25.5% of Play speed. DATA-1
        ///     contains an 8-bit speed value. The deviation from nominal Play speed is: Deviation(%)=0.1 x speed value
        /// </summary>
        public ProgSpeedPlayPlus()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.ProgSpeedPlayPlus;
        }
    }
}
