namespace Sony9Pin.net.CommandBlocks.SenseRequest
{
    /// <summary>
    /// 
    /// </summary>
    public class StatusSense : CommandBlock
    {
        /// <summary>
        /// </summary>
        public StatusSense(int length = 10) : this(0, length)
        {
        }

        /// <summary>
        /// </summary>
        public StatusSense(int start, int length)
        {
            var b = (byte)((((byte)start) << 4) | ((byte)length));
            var data = new[] { b };
            
            Cmd1 = Cmd1.SenseRequest;
            DataCount = data.Length;
            Cmd2 = (byte)SenseRequest.StatusSense;
            Data = data;
        }
    }
}
