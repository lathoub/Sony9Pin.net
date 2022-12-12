namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Eject : CommandBlock
    {
        /// <summary>
        ///     When this command is received, the _slave will Eject the tape.
        /// </summary>
        public Eject()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Eject;
        }
    }
}
