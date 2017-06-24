namespace Acme.Sony9Pin.CommandBlocks.SenseRequest
{
    /// <summary>
    ///     The sense request.
    /// </summary>
    public enum SenseRequest : byte
    {
        /// <summary>
        ///     The tc gen sense.
        /// </summary>
        TcGenSense = 0x0A,

        /// <summary>
        ///     The current time sense.
        /// </summary>
        CurrentTimeSense = 0x0C,

        /// <summary>
        ///     The status sense.
        /// </summary>
        StatusSense = 0x20,
    }
}