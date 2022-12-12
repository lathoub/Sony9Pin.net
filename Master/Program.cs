// See https://aka.ms/new-console-template for more information

using Sony9Pin.net;

Console.WriteLine("Discovering devices");

var slaves = Sony9PinSlave.Discover();
if (slaves.Length > 0)
foreach (var slave in slaves)
{
    Console.WriteLine(slave);
}
else
    Console.WriteLine("no devices found");
