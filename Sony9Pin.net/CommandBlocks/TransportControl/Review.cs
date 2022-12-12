namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Review : CommandBlock
    {
        /// <summary>
        ///     When one of these commands is received the _slave goes into the indicated mode
        /// </summary>
        public Review()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Review;
        }
    }
}
