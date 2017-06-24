namespace Acme.Sony9Pin.CommandBlocks.Return
{
    /// <summary>
    ///     The return.
    /// </summary>
    public enum Return : byte
    {
        /// <summary>
        ///     The ack.
        /// </summary>
        Ack = 0x01,

        /// <summary>
        ///     The device type.
        /// </summary>
        DeviceType = 0x11,

        /// <summary>
        ///     The nak.
        /// </summary>
        Nak = 0x12,
    }
}