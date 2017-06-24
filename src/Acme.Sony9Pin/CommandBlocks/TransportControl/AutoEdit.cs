namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoEdit : CommandBlock
    {
        /// <summary>
        ///     When one of these commands is received the _slave goes into the indicated mode
        /// </summary>
        public AutoEdit()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.AutoEdit;
        }
    }
}