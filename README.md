# Sony9Pin.net [![Build Status](https://travis-ci.org/lathoub/Sony9Pin.net.svg?branch=master)](https://travis-ci.org/lathoub/Sony9Pin.net) [![License: CC BY-SA 4.0](https://img.shields.io/badge/License-CC%20BY--SA%204.0-lightgrey.svg)](http://creativecommons.org/licenses/by-sa/4.0/) 
A .Net (C#) library to control a video recorder using the Sony 9-Pin protocol. The Odetics protocol is a superset of the Sony 9pin protocol. 
## Features
* Sony9Pin Master
* Sony9Pin Slave (requires additional programming)
* Odetics Master
* Odetics Slave (requires additional programming)
## Configuration
## Hardware
| Pin                 | Master           | Slave  |
| ------------------- |:----------------:|:-----:|
| 1 | Frame ground    | Frame ground |
| 2 | Receive A       |   Transmit A |
| 3 | Transmit B      |    Receive B |
| 4 | Transmit common | Receive common |
| 5 | Spare or ground |   Spare or ground |
| 6 | Receive common  |    Transmit common |
| 7 | Receive B       | Transmit B |
| 8 | Transmit A      |   Receive A |
| 9 | Frame ground    |    Frame ground |
