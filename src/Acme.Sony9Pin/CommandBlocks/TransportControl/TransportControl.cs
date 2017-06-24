namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    ///     The transport control.
    /// </summary>
    public enum TransportControl : byte
    {
        /// <summary>
        ///     The stop.
        /// </summary>
        Stop = 0x00,

        /// <summary>
        ///     The play.
        /// </summary>
        Play = 0x01,

        /// <summary>
        ///     The record.
        /// </summary>
        Record = 0x02,

        /// <summary>
        ///     The standby off.
        /// </summary>
        StandbyOff = 0x04,

        /// <summary>
        ///     The standby on.
        /// </summary>
        StandbyOn = 0x05,

        /// <summary>
        ///     The eject.
        /// </summary>
        Eject = 0x0F,

        /// <summary>
        ///     The fast fwd.
        /// </summary>
        FastFwd = 0x10,

        /// <summary>
        ///     The jog fwd.
        /// </summary>
        JogFwd = 0x11,

        /// <summary>
        ///     The var fwd.
        /// </summary>
        VarFwd = 0x12,

        /// <summary>
        ///     The shuttle fwd.
        /// </summary>
        ShuttleFwd = 0x13,

        /// <summary>
        ///     The in data preset.
        /// </summary>
        InDataPreset = 0x14,

        /// <summary>
        ///     The out data preset.
        /// </summary>
        OutDataPreset = 0x15,

        /// <summary>
        ///     The audio in data preset.
        /// </summary>
        AudioInDataPreset = 0x16,

        /// <summary>
        ///     The audio out data preset.
        /// </summary>
        AudioOutDataPreset = 0x17,

        /// <summary>
        ///     The rewind.
        /// </summary>
        Rewind = 0x20,

        /// <summary>
        ///     The jog rev.
        /// </summary>
        JogRev = 0x21,

        /// <summary>
        ///     The var rev.
        /// </summary>
        VarRev = 0x22,

        /// <summary>
        ///     The shuttle rev.
        /// </summary>
        ShuttleRev = 0x23,

        /// <summary>
        ///     The preroll.
        /// </summary>
        Preroll = 0x30,

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        CueUpWithData = 0x31,

        /// <summary>
        ///     The sync play.
        /// </summary>
        SyncPlay = 0x34,

        /// <summary>
        ///     The prog speed play plus.
        /// </summary>
        ProgSpeedPlayPlus = 0x38,

        /// <summary>
        ///     The prog speed play min.
        /// </summary>
        ProgSpeedPlayMin = 0x39,

        /// <summary>
        ///     The preview.
        /// </summary>
        Preview = 0x40,

        /// <summary>
        ///     The review.
        /// </summary>
        Review = 0x41,

        /// <summary>
        ///     The auto edit.
        /// </summary>
        AutoEdit = 0x42,

        /// <summary>
        ///     The outpoint preview.
        /// </summary>
        OutpointPreview = 0x43,

        /// <summary>
        ///     The anti clog timer disable.
        /// </summary>
        AntiClogTimerDisable = 0x54,

        /// <summary>
        ///     The anti clog timer enable.
        /// </summary>
        AntiClogTimerEnable = 0x55,

        /// <summary>
        ///     The full ee off.
        /// </summary>
        FullEEOff = 0x60,

        /// <summary>
        ///     The full ee on.
        /// </summary>
        FullEEOn = 0x61,

        /// <summary>
        ///     The select ee on.
        /// </summary>
        SelectEEOn = 0x63,

        /// <summary>
        ///     The edit off.
        /// </summary>
        EditOff = 0x64,

        /// <summary>
        ///     The edit on.
        /// </summary>
        EditOn = 0x65,

        /// <summary>
        ///     The freeze off.
        /// </summary>
        FreezeOff = 0x6A,

        /// <summary>
        ///     The freeze on.
        /// </summary>
        FreezeOn = 0x6A,
    }
}