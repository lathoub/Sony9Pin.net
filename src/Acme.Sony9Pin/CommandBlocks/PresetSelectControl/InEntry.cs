namespace Acme.Sony9Pin.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// 
    /// </summary>
    public class InEntry : CommandBlock
    {
        /// <summary>
        ///     Sets the Video in point to the value displayed on the _slave. This is the value of the selected tape timer.
        /// </summary>
        public InEntry()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)PresetSelectControl.InEntry;
        }
    }
}
