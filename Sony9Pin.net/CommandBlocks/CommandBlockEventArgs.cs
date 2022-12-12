namespace Sony9Pin.net.CommandBlocks
{
    /// <summary>
    ///     The device type event args.
    /// </summary>
    public class CommandBlockEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///     The device name.
        /// </summary>
        public readonly CommandBlock CommandBlock;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBlockEventArgs"/> class.
        /// </summary>
        /// <param name="commandBlock">
        /// The device name.
        /// </param>
        public CommandBlockEventArgs(CommandBlock commandBlock)
        {
            CommandBlock = commandBlock;
        }

        #endregion
    }
}