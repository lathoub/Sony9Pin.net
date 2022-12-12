namespace Sony9Pin.net.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// 
    /// </summary>
    public class InFlagReset : CommandBlock
    {
        /// <summary>
        ///     Sets the Video in point to the value displayed on the _slave. This is the value of the selected tape timer.
        /// </summary>
        public InFlagReset()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)PresetSelectControl.InFlagReset;
        }
    }
}
