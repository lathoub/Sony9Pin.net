namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class ShuttleFwd : CommandBlock
    {
        /// <summary>
        ///     When these commands are received the _slave device will move forward with the speed indicated by DATA-1 and DATA-2.
        /// </summary>
        public ShuttleFwd(byte n)
        {
            Cmd1 = Cmd1.TransportControl;
            DataCount = 1;
            Cmd2 = (byte)TransportControl.ShuttleFwd;
            Data = new[] { n };
        }

        /// <summary>
        ///     When these commands are received the _slave device will move forward with the speed indicated by DATA-1 and DATA-2.
        /// </summary>
        public ShuttleFwd(byte n1, byte n2)
        {
            Cmd1 = Cmd1.TransportControl;
            DataCount = 2;
            Cmd2 = (byte)TransportControl.ShuttleFwd;
            Data = new[] { n1, n2 };
        }
    }
}
