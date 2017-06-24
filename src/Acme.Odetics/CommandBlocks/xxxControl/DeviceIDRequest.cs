using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.xxxControl
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