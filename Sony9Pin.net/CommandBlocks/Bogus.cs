namespace Sony9Pin.net.CommandBlocks
{
    /// <summary>
    /// 
    /// </summary>
    public class Bogus : CommandBlock
    {
        /// <summary>
        ///     Whatever, should provoke a NAK
        /// /// </summary>
        public Bogus()
        {
            Cmd1 = (Cmd1)0xFA;
            Cmd2 = (byte)0xFA;
        }
    }
}
