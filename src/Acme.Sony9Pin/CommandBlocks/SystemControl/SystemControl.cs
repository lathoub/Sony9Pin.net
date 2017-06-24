namespace Acme.Sony9Pin.CommandBlocks.SystemControl
{
    /// <summary>
    ///     The system control.
    /// </summary>
    public enum SystemControl : byte
    {
        /// <summary>
        ///     The local disable.
        /// </summary>
        LocalDisable = 0x0C,

        /// <summary>
        ///     The device type request.
        /// </summary>
        DeviceTypeRequest = 0x11,

        /// <summary>
        ///     The local enable.
        /// </summary>
        LocalEnable = 0x1D,
    }
}