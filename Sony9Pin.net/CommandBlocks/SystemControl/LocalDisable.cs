namespace Sony9Pin.net.CommandBlocks.SystemControl
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalDisable : CommandBlock
    {
        /// <summary>
        ///     Disable operation of the _slave device from its control panel
        /// </summary>
        public LocalDisable()
        {
            Cmd1 = Cmd1.SystemControl;
            Cmd2 = (byte)SystemControl.LocalDisable;
        }
    }
}