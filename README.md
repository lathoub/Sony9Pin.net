# Sony9Pin.net [![Build Status](https://travis-ci.org/lathoub/Sony9Pin.net.svg?branch=master)](https://travis-ci.org/lathoub/Sony9Pin.net) [![License: CC BY-SA 4.0](https://img.shields.io/badge/License-CC%20BY--SA%204.0-lightgrey.svg)](http://creativecommons.org/licenses/by-sa/4.0/) 
A .Net (C#) library to control a video recorder using the Sony 9-Pin protocol. The Odetics protocol is a superset of the Sony 9pin protocol. 

* Developed using Visual Studio Community 2017, Version 15.2 (26430.14) release. .Net Framework 4.6.1
* Developed using Visual Studio Community 2022, Version 17.4.2 release. .NET 6.0 LTR

The Odetics extension of the Sony9Pin protocol is also included in this repo.  It is a superset of the popular Sony 9-Pin VTR control protocol. Most of the added functionnalities are about “Clip” management by which the user can enqueue “Load Clip”/“Load Next Clip” commands.

## Features
* Sony9Pin Master
* Sony9Pin Slave (requires additional programming)
* Odetics Master
* Odetics Slave (requires additional programming)

## C# Usage

### Master
This implementation of the Sony9PinMaster automatically requests TimeCode and StatusData when the command queue is empty, the user does not need to program this her/himself. Commands issued using the Command method will be put on top of the queue. 

(sample below taken from the BVW75 code in the samples folder)
(RS-422 is in DTE mode)
```csharp
Sony9PinMaster master = new Sony9PinMaster();
master.Open("COM3");
master.Command(new StandbyOn());
master.Command(new Play());
//...
master.Close();
```

#### Discovering slave devices:

```csharp
var slaves = Sony9PinSlave.Discover();
```

#### Listening to events
```csharp
master.TimeDataChanged += OnTimeDataChanged;
master.StatusDataChanged += OnStatusDataChanged;

private void OnTimeDataChanged(object sender, TimeDataEventArgs e)
{
   Console.WriteLine(e.TimeCode.Hours);
}

private void OnStatusDataChanged(object sender, StatusDataEventArgs e)
{
   Console.WriteLine(e.StatusData.Play); 
}
```

### Slave
When in Slave mode, the library must react to the command given by the Master. The reactions to the commands will have to be programmed.
(RS-422 is in DCE mode)

Inherit from Sony9PinSlave and implement the missing members.

```csharp
public class BVW75 : Sony9PinSlave
    {
        protected override CommandBlock CurrentTimeSense(TimeSenseRequest timeSenseRequest)
        {
            throw new NotImplementedException();
        }

        protected override CommandBlock StatusSense(int start, int length)
        {
            throw new NotImplementedException();
        }

        protected override CommandBlock TcGenSense(TimeSenseRequest timeSenseRequest)
        {
            throw new NotImplementedException();
        }

    }
```

Note: The Slave implementation is not as mature as the Master implementation.

## Visual Studio Community 2017
![alt text](https://user-images.githubusercontent.com/4082369/27515695-c8b4d6ce-59a9-11e7-8c2b-0de8209e46eb.PNG "Sony9Pin.Net in Visual Studio 2017")

The dropbox just below the On/Off switch lists the serial devices that are linked to Sony9Pin slaves (if this list is empty, no devices are connected). When a COMx is visible, you can switch on the device by clicking in the checkBox.

## Communication Format
The protocol is based on the EIA RS-422-A signal standard, usually at 38.4 kBit/s. The data are sent as 1 start bit + 8 data bits + 1 parity bit + 1 stop bit. Parity is odd: the bitwise sum of data bits 0 -7 and the parity bit is an odd number.
## Command Block Format
The controlling device and the controlled device communicate through the interchange of command blocks. The bytes in each command block are assigned as follows: 
* CMD-1/DATA COUNT. CMD-1 is the upper 4 bits, DATA COUNT is the lower 4
* CMD-2
* DATA-1 up to DATA-N, where n is the value in data count 
* CHECKSUM

### CMD-1 
Indicates the function and direction of the command, according to: 
* 0: System control (Master->Slave) 
* 1: Return for 0,2, or 4 of cmd-1 (Slave->Master) 
* 2: Transport Control (Master->Slave) 
* 4: Preset/Select control (Master->Slave) 
* 6: Sense Request (Master->Slave) 
* 7: Sense Return (Slave->Master) 
### DATA COUNT 
Indicates the number of bytes ( max 15 ) inserted between CMD-2 and CHECKSUM 
### CMD-2 
Designates the command. Refer to the command table for definitions. Ex. CMD-1=0 and CMD-2=0C means LOCAL DISABLE. 
### DATA-1 to DATA-N 
Data which correspond to those indicated by the command. Refer to the command table for data formats. 
### CHECKSUM 
Lower eight bits of the sum of the bytes in the command block. 
## Communication Protocol
The protocol is initiated by the master. The slave should return a response within 9 msec. The response may be: 
* NAK + Error Data: Undefined command or communication error 
* COMMAND + Data: if Command requested data 
* ACK: if Command did not request data

The master should not send another command until receiving a response from the slave device. The master must also insure that no more than 10 msec lapses between bytes in a command block. The master must immediatly stop sending data when it receives a NAK + Error Data message. If the Error Data contains "Undefined Command" the master may immediatley send another command, otherwise it must wait at least 10 msec before sending another command. When the master does not receive a response from the slave within the 10 msec timeout, it may assume that communications have ceased and take appropriate measures. 

## Cabling
| Pin                 | Master           | Slave  |
| ------------------- |:----------------:|:-----:|
| 1 | Ground    | Ground |
| 2 | Receive A       |   Transmit A |
| 3 | Transmit B      |    Receive B |
| 4 | Transmit common | Receive common |
| 5 | Spare           |   Spare  |
| 6 | Receive common  |    Transmit common |
| 7 | Receive B       | Transmit B |
| 8 | Transmit A      |   Receive A |
| 9 | Ground          |    Ground |

## Hardware
### RS-422 Adapters
http://softio.com/pcie_pci_express_2014 Model # LF774KB 
