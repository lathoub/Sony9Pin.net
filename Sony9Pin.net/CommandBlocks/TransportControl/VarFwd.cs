namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class VarFwd : CommandBlock
    {
        /// <summary>
        ///     When these commands are received the _slave device will move forward with the speed indicated by DATA-1 and DATA-2.
        /// </summary>
        public VarFwd()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.VarFwd;
        }
    }
}
