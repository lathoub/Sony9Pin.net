using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class DeviceIDRequest : CommandBlock
    {
        public DeviceIDRequest()
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.DeviceIDRequest;
        }
    }
}