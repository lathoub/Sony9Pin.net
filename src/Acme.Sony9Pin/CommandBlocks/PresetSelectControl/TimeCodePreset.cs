namespace Acme.Sony9Pin.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeCodePreset : CommandBlock
    {
        /// <summary>
        ///     Sets the Time Code Generator value to the time code indicated by DATA-1 thru DATA-4. The Data format is as per the
        ///     24.31 Cue Up With Data Cmd1, with two additional bits to indicate Color Frame and Drop Frame mode as follows:
        /// </summary>
        public TimeCodePreset(TimeCode tc)
        {
            //var data = tc.

            //this.Cmd1 = Cmd1.PresetSelectControl;
            //this.DataCount = data.Length;
            //this.Cmd2 = (byte)PresetSelectControl.TimeCodePreset;
            //this.Data = data;
        }
    }
}
