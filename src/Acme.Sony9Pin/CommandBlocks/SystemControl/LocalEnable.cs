namespace Acme.Sony9Pin.CommandBlocks.SystemControl
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalEnable : CommandBlock
    {
        /// <summary>
        ///     Enable operation of _slave device from Local panel according to the
        ///     Local enable map set by the "4XB8" Local Key Map command
        /// </summary>
        public LocalEnable()
        {
            Cmd1 = Cmd1.SystemControl;
            Cmd2 = (byte)SystemControl.LocalEnable;
        }
    }
}