namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class CueUpWithData : CommandBlock
    {
        /// <summary>
        ///     Cues the _slave to the indicated time.
        /// </summary>
        public CueUpWithData(TimeCode tc)
        {
            var data = tc.ToBinaryCodedDecimal();

            Cmd1DataCount = ToCmd1DataCount(Cmd1.TransportControl, data.Length);
            Cmd2 = (byte)TransportControl.CueUpWithData;
            Data = data;
        }
    }
}
