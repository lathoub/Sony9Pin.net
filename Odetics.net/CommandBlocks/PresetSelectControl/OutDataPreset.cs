using Sony9Pin.net;
using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// </summary>
    public class OutDataPreset : CommandBlock
    {
        /// <summary>
        /// </summary>
        public OutDataPreset()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)Sony9Pin.net.CommandBlocks.TransportControl.TransportControl.OutDataPreset;
        }

        /// <summary>
        /// </summary>
        /// <param name="tc"></param>
        public OutDataPreset(TimeCode tc)
        {
            var data = tc.ToBinaryCodedDecimal();

            Cmd1DataCount = ToCmd1DataCount(Cmd1.PresetSelectControl, data.Length);
            Cmd2 = (byte)Sony9Pin.net.CommandBlocks.TransportControl.TransportControl.OutDataPreset;
            Data = data;
        }
    }
}
