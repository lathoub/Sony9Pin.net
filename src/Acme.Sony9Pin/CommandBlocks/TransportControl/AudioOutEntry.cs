namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AudioOutEntry : CommandBlock
    {
        /// <summary>
        ///     Sets the audio out point to the value displayed on the _slave. This is the value of the selected tape timer.
        /// </summary>
        public AudioOutEntry()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)PresetSelectControl.PresetSelectControl.AudioOutEntry;
        }
    }
}