namespace Acme.Sony9Pin.CommandBlocks.SystemControl
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeRequest : CommandBlock
    {
        /// <summary>
        ///     This response is returned for a Device Type Request (00.11) command. Two bytes of
        ///     Data will be returned with this response. The first byte is generally used as a category of
        ///     device, and the second byte is generally used as a model number for the device. Although
        ///     not officially specified, some Video cassette recorder implementations use the
        ///     least significant bit of the category byte of the device type to indicate if the device is
        ///     configured for NTSC (bit low) or PAL (bit high). Video disk recorder manufacturers
        ///     must assign a unique number for their category and models, and should be careful to
        ///     avoid conflicting with existing device types.
        /// </summary>
        public DeviceTypeRequest()
        {
            Cmd1 = Cmd1.SystemControl;
            Cmd2 = (byte)SystemControl.DeviceTypeRequest;
        }
    }
}
