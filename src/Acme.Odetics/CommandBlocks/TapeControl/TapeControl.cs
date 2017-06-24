namespace Acme.Odetics.CommandBlocks.TapeControl
{
    /// <summary>
    ///     The transport control.
    /// </summary>
    public enum TapeControl : byte
    {
        /// <summary>
        ///     
        /// </summary>
        TapeSubsystemStatusRequest = 0x40,

        UnloadTape = 0x43,

        RetrieveFromArchieve = 0x45,

        ListFirstArchiveId = 0x47,

        ListNextArchiveId = 0x48,

        LoadTape = 0x41,

        IsTransferOK = 0x48,

        FormatTape = 0x42,

        DeleteIdFromArchive = 0x46,

        CopyIdToArchive = 0x44,

        CopyIdFromTapeToTape = 0x47,

        AbortTapeActivity = 0x49,
    }
}
