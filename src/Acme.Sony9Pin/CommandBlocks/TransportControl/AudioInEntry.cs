namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class AudioInEntry : CommandBlock
    {
        /// <summary>
        ///     Sets the audio in point to the value displayed on the _slave. This is the value of the selected tape timer.
        /// </summary>
        public AudioInEntry()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)PresetSelectControl.PresetSelectControl.AudioInEntry;
        }
    }
}