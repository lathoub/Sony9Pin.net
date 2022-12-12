namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoModeOn : CommandBlock
    {
        /// <summary>
        /// </summary>
        public AutoModeOn()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)PresetSelectControl.PresetSelectControl.AutoModeOn;
        }
    }
}