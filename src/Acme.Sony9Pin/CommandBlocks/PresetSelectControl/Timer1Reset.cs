namespace Acme.Sony9Pin.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Timer1Reset : CommandBlock
    {
        /// <summary>
        ///     Sets the Timer-1 value to the time code indicated by DATA-1 thru DATA-4. For the time format see the 24.31 Cue Up
        ///     With Data command.
        /// </summary>
        public Timer1Reset()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)PresetSelectControl.Timer1Reset;
        }
    }
}
