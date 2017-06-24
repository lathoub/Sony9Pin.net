namespace Acme.Sony9Pin.CommandBlocks.SenseReturn
{
    /// <summary>
    ///     The sense return.
    /// </summary>
    public enum SenseReturn : byte
    {
        /// <summary>
        ///     The timer 1 data.
        /// </summary>
        Timer1Data = 0x00,

        /// <summary>
        ///     The timer 2 data.
        /// </summary>
        Timer2Data = 0x01,

        /// <summary>
        ///     The ltc time data.
        /// </summary>
        LtcTimeData = 0x04,

        /// <summary>
        ///     The user bits ltc data.
        /// </summary>
        UserBitsLtcData = 0x05,

        /// <summary>
        ///     The vitc time data.
        /// </summary>
        VitcTimeData = 0x06,

        /// <summary>
        ///     The user bits vitc data.
        /// </summary>
        UserBitsVitcData = 0x07,

        /// <summary>
        ///     The gen time data.
        /// </summary>
        GenTimeData = 0x08,

        /// <summary>
        ///     The gen user bits data.
        /// </summary>
        GenUserBitsData = 0x09,

        /// <summary>
        ///     The in data.
        /// </summary>
        InData = 0x10,

        /// <summary>
        ///     The out data.
        /// </summary>
        OutData = 0x11,

        /// <summary>
        ///     The audio in data.
        /// </summary>
        AudioInData = 0x12,

        /// <summary>
        ///     The audio out data.
        /// </summary>
        AudioOutData = 0x13,

        /// <summary>
        ///     The corrected ltc time data.
        /// </summary>
        CorrectedLtcTimeData = 0x14,

        /// <summary>
        ///     The hold ub ltc data.
        /// </summary>
        HoldUbLtcData = 0x15,

        /// <summary>
        ///     The hold vitc time data.
        /// </summary>
        HoldVitcTimeData = 0x16,

        /// <summary>
        ///     The hold ub vitc data.
        /// </summary>
        HoldUbVitcData = 0x17,

        /// <summary>
        ///     The status data.
        /// </summary>
        StatusData = 0x20,
    }
}