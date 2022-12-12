// See https://aka.ms/new-console-template for more information

using Sony9Pin.net;
using Sony9Pin.net.CommandBlocks.Return;

Console.WriteLine("Discovering devices");

var slaves = Sony9PinSlave.Discover();
if (slaves.Length > 0)
foreach (var slave in slaves)
{
    Console.WriteLine(slave);
}
else
{
    Console.WriteLine("no devices found");
    return;
}

Sony9PinMaster master = new();

master.DeviceType += (o, e) => { Console.WriteLine(e.DeviceName); };
master.ConnectedChanged += (o, e) => { Console.WriteLine(e.Connected); };
master.TimeDataChanged += (o, e) => { Console.WriteLine(e.TimeCode); };
master.StatusDataChanged += (o, e) => { Console.WriteLine(e.StatusData); };

master.Open(slaves[0]);
