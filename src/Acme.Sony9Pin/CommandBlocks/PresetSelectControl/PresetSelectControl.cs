namespace Acme.Sony9Pin.CommandBlocks.PresetSelectControl
{
    /// <summary>
    ///     The preset select control.
    /// </summary>
    public enum PresetSelectControl : byte
    {
        /// <summary>
        ///     The timer 1 reset.
        /// </summary>
        TimeCodePreset = 0x04,

        /// <summary>
        ///     The timer 1 reset.
        /// </summary>
        Timer1Reset = 0x08,

        /// <summary>
        ///     The in entry.
        /// </summary>
        InEntry = 0x10,

        /// <summary>
        ///     The out entry.
        /// </summary>
        OutEntry = 0x11,

        /// <summary>
        ///     The audio in entry.
        /// </summary>
        AudioInEntry = 0x12,

        /// <summary>
        ///     The audio out entry.
        /// </summary>
        AudioOutEntry = 0x13,

        /// <summary>
        ///     The in data preset.
        /// </summary>
        InDataPreset = 0x14,

        /// <summary>
        ///     The out data preset.
        /// </summary>
        OutDataPreset = 0x15,

        /// <summary>
        ///     The in plus shift.
        /// </summary>
        InPlusShift = 0x18,

        /// <summary>
        ///     The in minus shift.
        /// </summary>
        InMinusShift = 0x19,

        /// <summary>
        ///     The out plus shift.
        /// </summary>
        OutPlusShift = 0x1A,

        /// <summary>
        ///     The out minus shift.
        /// </summary>
        OutMinusShift = 0x1B,

        /// <summary>
        ///     The in flag reset.
        /// </summary>
        InFlagReset = 0x20,

        /// <summary>
        ///     The out flag reset.
        /// </summary>
        OutFlagReset = 0x21,

        /// <summary>
        ///     The auto mode off.
        /// </summary>
        AutoModeOff = 0x40,

        /// <summary>
        ///     The auto mode on.
        /// </summary>
        AutoModeOn = 0x41,
    }
}