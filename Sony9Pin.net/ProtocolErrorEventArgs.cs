namespace Sony9Pin.net
{
    /// <summary>
    ///     The protocol error event args.
    /// </summary>
    public class ProtocolErrorEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///     The index.
        /// </summary>
        public readonly int Index;

        /// <summary>
        ///     The memory.
        /// </summary>
        public readonly byte[] Memory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProtocolErrorEventArgs" /> class.
        /// </summary>
        /// <param name="memory">
        ///     The memory.
        /// </param>
        /// <param name="index">
        ///     The index.
        /// </param>
        public ProtocolErrorEventArgs(byte[] memory, int index)
        {
            Memory = memory;
            Index = index;
        }

        #endregion
    }
}