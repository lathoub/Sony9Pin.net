namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class InDataPreset : CommandBlock
    {
        /// <summary>
        ///     Set the Video In Point to the value indicated by DATA-1 thru DATA-4. The time format is as per the 24.31 Cue Up
        ///     With Data command.
        /// </summary>
        public InDataPreset(TimeCode tc)
        {
            var data = tc.ToBinaryCodedDecimal();

            Cmd1DataCount = ToCmd1DataCount(Cmd1.PresetSelectControl, data.Length);
            Cmd2 = (byte)PresetSelectControl.PresetSelectControl.InDataPreset;
            Data = data;
        }
    }
}
