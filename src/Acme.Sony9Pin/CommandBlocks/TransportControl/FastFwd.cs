namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class FastFwd : CommandBlock
    {
        /// <summary>
        ///     When this command is received, the _slave device will run in fast forward mode. The speed depends on the VTR; for
        ///     the DVR2000 series it is 50 x Play speed.
        /// </summary>
        public FastFwd()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.FastFwd;
        }
    }
}