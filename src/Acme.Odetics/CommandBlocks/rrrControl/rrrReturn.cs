namespace Acme.Odetics.CommandBlocks.rrrControl
{
    /// <summary>
    ///     The transport control.
    /// </summary>
    public enum rrrReturn : byte
    {
        /// <summary>
        ///     
        /// </summary>
        IDListing = 0x14,

        /// <summary>
        ///     
        /// </summary>
        IDStatus = 0x18,

        /// <summary>
        ///     
        /// </summary>
        LongestContiguousAvailableStorage = 0x1C,

        /// <summary>
        ///     
        /// </summary>
        DeviceID = 0x21,
    }
}
