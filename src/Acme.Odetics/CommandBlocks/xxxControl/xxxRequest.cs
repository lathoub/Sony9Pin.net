namespace Acme.Odetics.CommandBlocks.xxxControl
{
    /// <summary>
    ///     The transport control.
    /// </summary>
    public enum xxxRequest : byte
    {
        /// <summary>
        ///     
        /// </summary>
        AutoSkip = 0x01,

        RecordCueUpWithData = 0x02,

        PreviewInPreset = 0x04,

        PreviewOutPreset = 0x05,

        PreviewInReset = 0x06,

        PreviewOutReset = 0x07,

        EraseID = 0x10,

        EraseSegment = 0x11,

        ListFirstID = 0x14,

        ListNextID = 0x15,

        IDStatusRequest = 0x18,

        LongestContiguousStorageRequest = 0x1C,

        SetDeviceID = 0x20,

        DeviceIDRequest = 0x21,
    }
}
