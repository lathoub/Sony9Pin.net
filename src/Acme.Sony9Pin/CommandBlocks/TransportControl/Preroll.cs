namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Preroll : CommandBlock
    {
        /// <summary>
        ///     When this command is received the _slave will search to the Preroll position defined as the value obtained by
        ///     subtracting the Preroll time set by the 44.31 Preroll Time Preset command from the IN POINT Data stored in the IN
        ///     ENTRY memory by the 40.10 In Entry command.
        /// </summary>
        public Preroll()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Preroll;
        }
    }
}
