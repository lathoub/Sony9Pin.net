namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class ShuttleRev : CommandBlock
    {
        /// <summary>
        ///     When receiving one of the above commands, the _slave will start running in accordance with the speed Data defined
        ///     by DATA-1 and DATA-2. For the maximum and minimum speed see the 2X.12 Shuttle Fwd Cmd1.
        /// </summary>
        public ShuttleRev(byte n)
        {
            Cmd1 = Cmd1.TransportControl;
            DataCount = 1;
            Cmd2 = (byte)TransportControl.ShuttleRev;
            Data = new[] { n };
        }

        /// <summary>
        ///     When receiving one of the above commands, the _slave will start running in accordance with the speed Data defined
        ///     by DATA-1 and DATA-2. For the maximum and minimum speed see the 2X.12 Shuttle Fwd Cmd1.
        /// </summary>
        public ShuttleRev(byte n1, byte n2)
        {
            Cmd1 = Cmd1.TransportControl;
            DataCount = 2;
            Cmd2 = (byte)TransportControl.ShuttleRev;
            Data = new[] { n1, n2 };
        }
    }
}
