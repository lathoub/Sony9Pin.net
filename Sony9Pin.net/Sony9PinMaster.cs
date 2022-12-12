// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sony9PinMaster.cs" company="Acme">
//   2013 Acme
// </copyright>
// <summary>
//   The sony 9 pin master.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using Sony9Pin.net.CommandBlocks;
using Sony9Pin.net.CommandBlocks.Return;
using Sony9Pin.net.CommandBlocks.SenseRequest;
using Sony9Pin.net.CommandBlocks.SenseReturn;
using Sony9Pin.net.CommandBlocks.StatusData;
using Sony9Pin.net.CommandBlocks.SystemControl;
using Sony9Pin.net.CommandBlocks.TimeSenseRequest;

namespace Sony9Pin.net
{
    /// <summary>
    ///     The sony 9 pin master.
    ///     The protocol is initiated by the master. The slave should return a response within 9 msec. The response may be:
    ///     NAK + Error Data: Undefined Cmd1 or communication error
    ///     COMMAND + Data: if Cmd1 requested data
    ///     ACK: if Cmd1 did not request data
    ///     The master should not send another Cmd1 until receiving a response from the slave device. The master must also
    ///     insure that no more than 10 msec lapses between bytes in a Cmd1 block. The master must immediately stop sending
    ///     data when it receives a NAK + Error Data message. If the Error Data contains "Undefined Cmd1" the master may
    ///     immediately send another Cmd1, otherwise it must wait at least 10 msec before sending another Cmd1. When
    ///     the master does not receive a response from the slave within the 10 msec timeout, it may assume that communications
    ///     have ceased and take appropriate measures.
    /// </summary>
    public class Sony9PinMaster : Sony9Pin
    {
        #region Fields

        private BackgroundWorker? _queueReaderWorker;

        private readonly AutoResetEvent _workerThreadStopped = new(false);

        /// <summary>
        ///     The _command blocks.
        /// </summary>
        private BlockingCollection<CommandBlock>? _commandBlocks;

        private bool _connected;

        private CancellationTokenSource? _packetsCancellationTokenSource;

        /// <summary>
        /// </summary>
        private StatusData? _statusData = new();

        /// <summary>
        /// 
        /// </summary>
        public StatusData? StatusData
        {
            get
            {
                lock (this)
                {
                    return _statusData;
                }
            }
        }

        /// <summary>
        /// </summary>
        private TimeCode? _timeCode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Constructor
        ///     Initializes a new instance of the <see cref="Sony9PinMaster" /> class.
        /// </summary>
        public Sony9PinMaster()
        {
            // milliseconds before a timeout occurs.
            // According to the Sony9Pin spec this is 9ms.
            ResponseTimeout = 9;

            DeviceName = "Unknown";
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     The acknowledgement.
        /// </summary>
        public event EventHandler? Ack;

        /// <summary>
        ///     The connected.
        /// </summary>
        public event EventHandler<ConnectedEventArgs>? ConnectedChanged;

        /// <summary>
        ///     The device type changed.
        /// </summary>
        public event EventHandler<DeviceTypeEventArgs>? DeviceType;

        /// <summary>
        ///     The not acknowledgement.
        /// </summary>
        public event EventHandler? Nak;

        // public event TimeDataHandler InData;
        // public event TimeDataHandler OutData;
        // public event TimeDataHandler AudioInData;
        // public event TimeDataHandler AudioOutData;

        /// <summary>
        ///     The status data.
        /// </summary>
        public event EventHandler<StatusDataEventArgs>? StatusDataReceived;

        /// <summary>
        ///     The status data.
        /// </summary>
        public event EventHandler<StatusDataEventArgs>? StatusDataChanged;

        /// <summary>
        ///     The status data.
        /// </summary>
        public event EventHandler<StatusDataEventArgs>? StatusDataChanging;

        /// <summary>
        ///     The time data.
        /// </summary>
        public event EventHandler<TimeDataEventArgs>? TimeDataReceived;

        /// <summary>
        ///     The time data.
        /// </summary>
        public event EventHandler<TimeDataEventArgs>? TimeDataChanged;

        /// <summary>
        ///     The time data.
        /// </summary>
        public event EventHandler<TimeDataEventArgs>? TimeDataChanging;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the device name.
        /// </summary>
        public string? DeviceName { get; private set; }

        // public event TimeDataHandler ExtendedStatusData;
        // public event TimeDataHandler SignalControlData;
        // public event TimeDataHandler LocalKeyMap;
        // public event TimeDataHandler HeadMeterData;
        // public event TimeDataHandler RemainingTime;
        // public event TimeDataHandler CmdSpeedData;
        // public event TimeDataHandler EditPresetStatus;
        // public event TimeDataHandler PrerollTime;
        // public event TimeDataHandler TimerModeStatus;
        // public event TimeDataHandler RecordInhibitStatus;
        // public event TimeDataHandler DAInputEmphasisData;
        // public event TimeDataHandler DAPlaybackEmphasisData;
        // public event TimeDataHandler DASamplingFrequencyData;
        // public event TimeDataHandler CrossFadeTimeData;

        /// <summary>
        ///     Gets or sets the port name.
        /// </summary>
        public bool IsConnected
        {
            get => _connected;

            set
            {
                if (value == _connected) return;

                _connected = value;
                RaiseConnectedChanged(_connected);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected int ResponseTimeout
        {
            get => Serial.ReadTimeout;
            set => Serial.ReadTimeout = value;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Close the serial port
        /// </summary>
        public override void Close()
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster Closing");

            // Closes serial port and flushes buffers
            base.Close();

            // Let the this._QueueReaderWorker know its time to finish
            // Doesn't matter is the tread is busy or not (ég is Open was not successfull, the
            // QueueReaderWorker is not started and  IsBusy will be false)
            if (_queueReaderWorker is { WorkerSupportsCancellation: true })
            {
                _queueReaderWorker.CancelAsync();
            }

            // Raise the token to cancel the Blocking IO call on the commandBlock collection
            if (null != _packetsCancellationTokenSource)
            {
                while (_commandBlocks is { Count: > 0 })
                    _commandBlocks.Take();

                _packetsCancellationTokenSource.Cancel();
            }

            // Indicate no more blocks will be added from now on
            _commandBlocks?.CompleteAdding();

            // Wait here for worker thread to signal its done
            if (null != _queueReaderWorker)
            {
                if (_queueReaderWorker.IsBusy)
                    if (!_workerThreadStopped.WaitOne()) // will block until _workerThreadStopped.Set() call made at end of worker thread
                        Trace.TraceError("Could not stop serial master thread");

                _queueReaderWorker.Dispose();
                _queueReaderWorker = null;
            }

            // Cleanup the commandBlock collection
            if (null != _commandBlocks)
            {
                _commandBlocks.Dispose();
                _commandBlocks = null;
            }

            // Cleanup the token
            if (null != _packetsCancellationTokenSource)
            {
                _packetsCancellationTokenSource.Dispose();
                _packetsCancellationTokenSource = null;
            }

            _statusData = null;
            _timeCode = null;

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster Closed");
        }

        /// <summary>
        /// </summary>
        /// <param name="command">
        /// </param>
        public void Command(CommandBlock command)
        {
            // do not pass 'null' as last parameter, but 'new byte[0]' to prevent errors
            Command(command.Cmd1DataCount, command.Cmd2, command.Data);
        }

        /// <summary>
        /// </summary>
        /// <param name="cmd1">
        /// </param>
        /// <param name="dataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        /// <param name="data">
        /// </param>
        protected void Command(Cmd1 cmd1, int dataCount, byte cmd2, byte[] data)
        {
            Command(CommandBlock.ToCmd1DataCount(cmd1, dataCount), cmd2, data);
        }

        /// <summary>
        /// </summary>
        /// <param name="cmd1">
        /// </param>
        /// <param name="dataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        protected void Command(Cmd1 cmd1, int dataCount, byte cmd2)
        {
            Command(CommandBlock.ToCmd1DataCount(cmd1, dataCount), cmd2, Array.Empty<byte>());
        }

        /// <summary>
        /// </summary>
        /// <param name="cmd1DataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        protected void Command(byte cmd1DataCount, byte cmd2)
        {
            // do not pass 'null' as last parameter, but 'new byte[0]' to prevent errors
            Command(cmd1DataCount, cmd2, Array.Empty<byte>());
        }

        /// <summary>
        ///     The Master of the communication (a remote controller) requests
        ///     a Cmd1 to be executed
        /// </summary>
        /// <param name="cmd1DataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        /// <param name="data">
        /// </param>
        protected void Command(byte cmd1DataCount, byte cmd2, byte[] data)
        {
            // Simple protocol check. The lower tupple in Cmd1DataCount must
            // match the length of Data
            var dataCount = cmd1DataCount & 0x0F;
            if (dataCount != data.Length)
            {
                throw new ProtocolViolationException("Length provider in the Cmd1DataCount does not match the length of the data provided.");
            }

            // Sense Cmd1 always go through, regardsless or not
            // we enabled.
            switch (CommandBlock.GetCmd1(cmd1DataCount))
            {
                case Cmd1.SystemControl:
                case Cmd1.Return:
                case Cmd1.TransportControl:
                    break;
                case Cmd1.SenseRequest:
                case Cmd1.SenseReturn:
                    break;
            }

            // Lock this object, it can also be access from the receiving thread.
            if (_commandBlocks is { IsAddingCompleted: false })
            {
                var commandBlock = new CommandBlock(cmd1DataCount, cmd2, data);
                // Place this message on the stack, so that it can be send.
                // It will be picked up by the automatically by the send-receive loop.
                _commandBlocks.Add(commandBlock);

                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Requesting " + commandBlock);
            }
        }

        /// <summary>
        /// </summary>
        public override bool Open(string portName)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster Opening");

            if (!base.Open(portName))
            {
                return false;
            }

            _queueReaderWorker = new BackgroundWorker { WorkerReportsProgress = false, WorkerSupportsCancellation = true };
            _queueReaderWorker.DoWork += QueueReader;
            _queueReaderWorker.RunWorkerCompleted += OnQueueReaderCompleted;

            _packetsCancellationTokenSource = new CancellationTokenSource();
            _commandBlocks = new BlockingCollection<CommandBlock>();

            // Request DeviceType to kick off the conversation
            Command(new DeviceTypeRequest());

            // Setup background worker.
            _queueReaderWorker.RunWorkerAsync(); // and start async!

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster Opened");

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="response">
        ///     The response.
        /// </param>
        protected virtual void ProcessResponse(CommandBlock? response)
        {
            if (null == response)
            {
                return;
            }

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Response: " + response);

            switch (response.Cmd1)
            {
                case Cmd1.Return:
                    switch ((Return)response.Cmd2)
                    {
                        case Return.Ack:
                            // Indication that last Cmd1 was alright
                            RaiseAckHandler();
                            break;

                        case Return.Nak:
                            // The master must immediately stop sending data when it receives a NAK + Error Data message.
                            //this.Enabled = false;

                            var bits = new BitArray(new int[] { response.Data[0] });
                            if (bits.Get((int)NakCommandBlock.Nak.ChecksumError))
                            {
                                Thread.Sleep(10);
                            }
                            if (bits.Get((int)NakCommandBlock.Nak.FrameError))
                            {
                                Thread.Sleep(10);
                            }
                            if (bits.Get((int)NakCommandBlock.Nak.OverrunError))
                            {
                                Thread.Sleep(10);
                            }
                            if (bits.Get((int)NakCommandBlock.Nak.ParityError))
                            {
                                Thread.Sleep(10);
                            }
                            if (bits.Get((int)NakCommandBlock.Nak.TimeOut))
                            {
                                Thread.Sleep(10);
                            }
                            if (bits.Get((int)NakCommandBlock.Nak.UndefinedError))
                            {
                            }
                            RaiseNakHandler((NakCommandBlock.Nak)response.Data[0]);
                            break;

                        case Return.DeviceType:
                            {
                                var deviceId = (ushort)((response.Data[0] << 8) | response.Data[1]);
                                if (!Device.Names.TryGetValue(deviceId, out var deviceName))
                                {
                                    deviceName = BitConverter.ToString(response.Data).Replace("-", string.Empty);
                                }

                                DeviceName = deviceName;

                                RaiseDeviceTypeHandler(DeviceName);
                            }
                            break;
                    }

                    break;

                case Cmd1.SenseReturn:
                    switch ((SenseReturn)response.Cmd2)
                    {
                        case SenseReturn.Timer1Data:
                        case SenseReturn.Timer2Data:
                        case SenseReturn.LtcTimeData:
                        case SenseReturn.UserBitsLtcData:
                        case SenseReturn.VitcTimeData:
                        case SenseReturn.UserBitsVitcData:
                        case SenseReturn.GenTimeData:
                        case SenseReturn.GenUserBitsData:
                        case SenseReturn.CorrectedLtcTimeData:
                        case SenseReturn.HoldUbLtcData:
                        case SenseReturn.HoldVitcTimeData:
                        case SenseReturn.HoldUbVitcData:
                            var timeCode = new TimeCode(response.Data);
                            if (!timeCode.Equals(_timeCode))
                            {
                                RaiseTimeDataChangingHandler((SenseReturn)response.Cmd2, _timeCode);
                                _timeCode = timeCode;
                                RaiseTimeDataChangedHandler((SenseReturn)response.Cmd2, _timeCode);
                            }
                            RaiseTimeDataReceivedHandler((SenseReturn)response.Cmd2, _timeCode);
                            break;

                        case SenseReturn.StatusData:
                            var statusData = new StatusData(response.Data);
                            if (!statusData.Equals(_statusData))
                            {
                                RaiseStatusDataChangingHandler(_statusData);
                                lock (this)
                                {
                                    _statusData = statusData;
                                }
                                RaiseStatusDataChangedHandler(_statusData);
                            }
                            RaiseStatusDataReceivedHandler(_statusData);
                           break;
                    }

                    break;
            }
        }

        /// <summary>
        ///     The raise ack handler.
        /// </summary>
        protected virtual void RaiseAckHandler()
        {
            var handler = Ack;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The raise connected.
        /// </summary>
        protected virtual void RaiseConnectedChanged(bool connected)
        {
            var handler = ConnectedChanged;
            handler?.Invoke(this, new ConnectedEventArgs(connected));
        }

        /// <summary>
        ///     The raise device type handler.
        /// </summary>
        /// <param name="deviceName">
        ///     The sender.
        /// </param>
        protected virtual void RaiseDeviceTypeHandler(string? deviceName)
        {
            var handler = DeviceType;
            handler?.Invoke(this, new DeviceTypeEventArgs(deviceName));
        }

        /// <summary>
        ///     The raise nak handler.
        /// </summary>
        protected virtual void RaiseNakHandler(NakCommandBlock.Nak error)
        {
            var handler = Nak;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The raise status data handler.
        /// </summary>
        /// <param name="statusData">
        ///     The sender.
        /// </param>
        protected virtual void RaiseStatusDataReceivedHandler(StatusData? statusData)
        {
            var handler = StatusDataReceived;
            handler?.Invoke(this, new StatusDataEventArgs(statusData));
        }

        /// <summary>
        ///     The raise status data handler.
        /// </summary>
        /// <param name="statusData">
        ///     The sender.
        /// </param>
        protected virtual void RaiseStatusDataChangedHandler(StatusData? statusData)
        {
            var handler = StatusDataChanged;
            handler?.Invoke(this, new StatusDataEventArgs(statusData));
        }

        /// <summary>
        ///     The raise status data handler.
        /// </summary>
        /// <param name="statusData">
        ///     The sender.
        /// </param>
        protected virtual void RaiseStatusDataChangingHandler(StatusData? statusData)
        {
            var handler = StatusDataChanging;
            handler?.Invoke(this, new StatusDataEventArgs(statusData));
        }

        /// <summary>
        ///     The raise time data handler.
        /// </summary>
        /// <param name="senseReturn">
        ///     The sender.
        /// </param>
        /// <param name="timeCode">
        ///     The sender.
        /// </param>
        protected virtual void RaiseTimeDataReceivedHandler(SenseReturn senseReturn, TimeCode? timeCode)
        {
            var handler = TimeDataReceived;
            handler?.Invoke(this, new TimeDataEventArgs(senseReturn, timeCode));
        }

        /// <summary>
        ///     The raise time data handler.
        /// </summary>
        /// <param name="senseReturn">
        ///     The sender.
        /// </param>
        /// <param name="timeCode">
        ///     The sender.
        /// </param>
        protected virtual void RaiseTimeDataChangedHandler(SenseReturn senseReturn, TimeCode? timeCode)
        {
            var handler = TimeDataChanged;
            handler?.Invoke(this, new TimeDataEventArgs(senseReturn, timeCode));
        }

        /// <summary>
        ///     The raise time data handler.
        /// </summary>
        /// <param name="senseReturn">
        ///     The sender.
        /// </param>
        /// <param name="timeCode">
        ///     The sender.
        /// </param>
        protected virtual void RaiseTimeDataChangingHandler(SenseReturn senseReturn, TimeCode? timeCode)
        {
            var handler = TimeDataChanging;
            handler?.Invoke(this, new TimeDataEventArgs(senseReturn, timeCode));
        }

        /// <summary>
        ///     Queue reader thread
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The ea.
        /// </param>
        private void QueueReader(object? sender, DoWorkEventArgs e)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster Starting QueueReader");

            if (sender is not BackgroundWorker worker)
                throw new ArgumentNullException(nameof(sender));

            // Make sure we have an empty buffer
            InputBuffer.Clear();

            while (_commandBlocks is { IsAddingCompleted: false })
            {
                if (worker.CancellationPending)
                    break;

                if (!Serial.IsOpen)
                    break;

                // If nothing is on the queue, get the TC and Status
                if (_commandBlocks.Count <= 0)
                    QueueStarved();

                try
                {
                    // Blocking call to get a CommandBlock
                    if (_packetsCancellationTokenSource != null)
                    {
                        var request = _commandBlocks.Take(_packetsCancellationTokenSource.Token);

                        // Flush serial buffers, both in and out
                        Serial.DiscardInBuffer();
                        Serial.DiscardOutBuffer();

                        // Send the CommandBlock out via the SerialPort
                        Send(request);
                    }

                    // Read characters from the serialPort until we can create a 
                    // complete and valid CommandBlock
                    while (Serial.IsOpen)
                    {
                        InputBuffer.Add((byte)Serial.ReadByte()); // With timeout !

                        try
                        {
                            if (!CommandBlock.TryParse(InputBuffer, out CommandBlock? response))
                                continue;
                            
                            IsConnected = true;

                            // OK, we have enough characters for a valid CommandBlock.
                            // Remove any unread characters from the serial buffers
                            // (there shouldn't be any, but you never know)
                            InputBuffer.Clear();

                            // Process the response from the slave.
                            ProcessResponse(response);

                            // We are done here, break back into the main loop to 
                            // try to take another CommandBlack
                            break;
                        }
                        catch (ArgumentException)
                        {
                            InputBuffer.Clear();
                        }
                    }
                }
                catch (IOException)
                {
                    break; // Leave after this error
                }
                catch (InvalidOperationException)
                {
                    break; // Leave after this error
                }
                catch (TimeoutException)
                {                    
                    // Oei - a character couldn't be read within the time given.

                    // Indicate here that we have been disconnected
                    IsConnected = false;

                    // Do not leave this while loop. Try to get a new message
                }
                catch (OperationCanceledException)
                {
                    break; // Leave after this error
                }
            }

            e.Cancel = true;

            _workerThreadStopped.Set(); // signal that worker is done

            IsConnected = false;

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster QueueReader Stopping");
        }

        private static int  _currentTimeSenseOrStatusSense;

        private void QueueStarved()
        {
            //Thread.Yield();
            Thread.Sleep(1);

            if (_currentTimeSenseOrStatusSense == 0)
            {
                Command(new CurrentTimeSense(TimeSenseRequest.LtcTime));
            }
            if (_currentTimeSenseOrStatusSense == 1)
            {
                Command(new StatusSense());
            }

            // Alternate between CurrentTimeSense and StatusSense
            _currentTimeSenseOrStatusSense++;
            if (_currentTimeSenseOrStatusSense > 1)
            {
                _currentTimeSenseOrStatusSense = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnQueueReaderCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinMaster QueueReader Stopped");
        }

        #endregion
    }
}