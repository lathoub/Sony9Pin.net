namespace Sony9Pin.net.CommandBlocks.SenseRequest
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentTimeSense : CommandBlock
    {
        /// <summary>
        /// </summary>
        public CurrentTimeSense(TimeSenseRequest.TimeSenseRequest request)
        {
            var data = new[] { (byte)request };
            
            Cmd1 = Cmd1.SenseRequest;
            DataCount = data.Length;
            Cmd2 = (byte)SenseRequest.CurrentTimeSense;
            Data = data;
        }
    }
}
