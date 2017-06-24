namespace Acme.Sony9Pin.CommandBlocks.Return
{
    /// <summary>
    ///     The device type.
    /// </summary>
    public class DeviceType : CommandBlock
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceType"/> class.
        /// </summary>
        /// <param name="deviceType">
        /// The device type.
        /// </param>
        public DeviceType(byte[] deviceType)
        {
            Cmd1 = Cmd1.Return;
            DataCount = 2;
            Cmd2 = (byte)Return.DeviceType;
            Data = deviceType;
        }

        #endregion
    }
}