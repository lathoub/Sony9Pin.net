namespace Sony9Pin.net.CommandBlocks.Return
{
    /// <summary>
    ///     The ack command block.
    /// </summary>
    public class AckCommandBlock : CommandBlock
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AckCommandBlock" /> class.
        /// </summary>
        public AckCommandBlock()
        {
            Cmd1 = Cmd1.Return;
            Cmd2 = (byte)Return.Ack;
        }

        #endregion
    }
}