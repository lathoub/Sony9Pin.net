namespace Sony9Pin.net.CommandBlocks.Return
{
    /// <summary>
    ///     The device type event args.
    /// </summary>
    public class DeviceTypeEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///     The device name.
        /// </summary>
        public readonly string? DeviceName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTypeEventArgs"/> class.
        /// </summary>
        /// <param name="deviceName">
        /// The device name.
        /// </param>
        public DeviceTypeEventArgs(string? deviceName)
        {
            DeviceName = deviceName;
        }

        #endregion
    }
}