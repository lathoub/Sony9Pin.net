namespace Acme.Sony9Pin.CommandBlocks.TimeSenseRequest
{
    /// <summary>
    ///     The time sense request.
    /// </summary>
    public enum TimeSenseRequest : byte
    {
        /// <summary>
        ///     The ltc time.
        /// </summary>
        LtcTime = 0x01,

        /// <summary>
        ///     The vitc time.
        /// </summary>
        VitcTime = 0x02,

        /// <summary>
        ///     The timer 1.
        /// </summary>
        Timer1 = 0x04,

        /// <summary>
        ///     The timer 2.
        /// </summary>
        Timer2 = 0x08,

        /// <summary>
        ///     The ltc user bits.
        /// </summary>
        LtcUserBits = 0x10,

        /// <summary>
        ///     The vitc user bits.
        /// </summary>
        VitcUserBits = 0x20,
    }
}