namespace Sony9Pin.net.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPlusShift : CommandBlock
    {
        /// <summary>
        ///     Increments the Video out point by one frame.
        /// </summary>
        public OutPlusShift()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)PresetSelectControl.OutPlusShift;
        }
    }
}
