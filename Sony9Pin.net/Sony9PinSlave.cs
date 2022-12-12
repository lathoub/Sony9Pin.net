// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sony9PinSlave.cs" company="Acme">
//   2013 Acme
// </copyright>
// <summary>
//   Defines the Sony9PinSlave type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO.Ports;
using Sony9Pin.net.CommandBlocks;
using Sony9Pin.net.CommandBlocks.Return;
using Sony9Pin.net.CommandBlocks.SenseRequest;
using Sony9Pin.net.CommandBlocks.SystemControl;
using Sony9Pin.net.CommandBlocks.TimeSenseRequest;
using Sony9Pin.net.CommandBlocks.TransportControl;

namespace Sony9Pin.net
{
    /// <summary>
    /// </summary>
    public abstract class Sony9PinSlave : Sony9Pin
    {
        #region Fields

        /// <summary>
        /// </summary>
        protected byte[] DeviceType = new byte[2];

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        protected Sony9PinSlave()
        {
        }

        /// <summary>
        /// </summary>
        protected Sony9PinSlave(byte[] deviceType)
            : this()
        {
            DeviceType = deviceType;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TimeCodeEventArgs>? CueUpWithData;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TimeCodeEventArgs>? InDataPreset;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TimeCodeEventArgs>? OutDataPreset;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Eject;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? FastFwd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? JogFwd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? JogRev;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? LocalDisable;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? LocalEnable;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Play;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Preroll;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Record;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Rewind;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? ShuttleFwd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? ShuttleRev;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? StandbyOff;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? StandbyOn;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Stop;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? VarFwd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? VarRev;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        public override void Close()
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinSlave Closing");

            Debug.Assert(null != Serial, "serialPort is null");
            Serial.DataReceived -= OnDataReceived;

            base.Close();

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinSlave Closed");
        }

        /// <summary>
        ///     Open the serial port
        /// </summary>
        /// <param name="portName">
        ///     The port Name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public override bool Open(string portName)
        {
            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinSlave Opening");

            Debug.Assert(null != Serial, "serialPort is null");
            Serial.DataReceived += OnDataReceived;
            Serial.ReceivedBytesThreshold = 1; // could this be 3?

            if (!base.Open(portName))
            {
                return false;
            }

            Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Sony9PinSlave Opened");

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     SenseRequest
        /// </summary>
        /// <param name="timeSenseRequest">
        ///     The time sense request.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected abstract CommandBlock? CurrentTimeSense(TimeSenseRequest timeSenseRequest);

        /// <summary>
        ///     The device type request.
        /// </summary>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected virtual CommandBlock? DeviceTypeRequest()
        {
            Debug.Assert(null != DeviceType, "DeviceType is null");

            return new DeviceType(DeviceType);
        }

        /// <summary>
        /// </summary>
        /// <param name="request">
        ///     The request.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected virtual CommandBlock? ProcessRequest(CommandBlock request)
        {
            CommandBlock? response = new AckCommandBlock();

            switch (request.Cmd1)
            {
                case Cmd1.SystemControl:
                    switch ((SystemControl)request.Cmd2)
                    {
                        case SystemControl.LocalDisable:
                            RaiseLocalDisable();
                            break;
                        case SystemControl.DeviceTypeRequest:
                            response = DeviceTypeRequest();
                            break;
                        case SystemControl.LocalEnable:
                            RaiseLocalEnable();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                case Cmd1.TransportControl:
                    switch ((TransportControl) request.Cmd2)
                    {
                        case TransportControl.Stop:
                            RaiseStop();
                            break;
                        case TransportControl.Play:
                            RaisePlay();
                            break;
                        case TransportControl.Record:
                            RaiseRecord();
                            break;
                        case TransportControl.StandbyOff:
                            RaiseStandbyOff();
                            break;
                        case TransportControl.StandbyOn:
                            RaiseStandbyOn();
                            break;
                        case TransportControl.Eject:
                            RaiseEject();
                            break;
                        case TransportControl.FastFwd:
                            RaiseFastFwd();
                            break;
                        case TransportControl.JogFwd:
                            RaiseJogFwd();
                            break;
                        case TransportControl.VarFwd:
                            RaiseVarFwd();
                            break;
                        case TransportControl.ShuttleFwd:
                            RaiseShuttleFwd();
                            break;
                        case TransportControl.Rewind:
                            RaiseRewind();
                            break;
                        case TransportControl.JogRev:
                            RaiseJogRev();
                            break;
                        case TransportControl.VarRev:
                            RaiseVarRev();
                            break;
                        case TransportControl.ShuttleRev:
                            RaiseShuttleRev();
                            break;
                        case TransportControl.Preroll:
                            RaisePreroll();
                            break;
                        case TransportControl.CueUpWithData:
                            RaiseCueUpWithData(request.Data);
                            break;
                        case TransportControl.InDataPreset:
                            RaiseInDataPreset(request.Data);
                            break;
                        case TransportControl.OutDataPreset:
                            RaiseOutDataPreset(request.Data);
                            break;
                        case TransportControl.AudioInDataPreset:
                            break;
                        case TransportControl.AudioOutDataPreset:
                            break;
                        case TransportControl.SyncPlay:
                            break;
                        case TransportControl.ProgSpeedPlayPlus:
                            break;
                        case TransportControl.ProgSpeedPlayMin:
                            break;
                        case TransportControl.Preview:
                            break;
                        case TransportControl.Review:
                            break;
                        case TransportControl.AutoEdit:
                            break;
                        case TransportControl.OutpointPreview:
                            break;
                        case TransportControl.AntiClogTimerDisable:
                            break;
                        case TransportControl.AntiClogTimerEnable:
                            break;
                        case TransportControl.FullEEOff:
                            break;
                        case TransportControl.FullEEOn:
                            break;
                        case TransportControl.SelectEEOn:
                            break;
                        case TransportControl.EditOff:
                            break;
                        case TransportControl.EditOn:
                            break;
                        case TransportControl.FreezeOff:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                case Cmd1.PresetSelectControl:
                    break;

                case Cmd1.SenseRequest:
                    switch ((SenseRequest) request.Cmd2)
                    {
                        case SenseRequest.TcGenSense:
                            response = TcGenSense((TimeSenseRequest) request.Data[0]);
                            break;
                        case SenseRequest.CurrentTimeSense:
                            response = CurrentTimeSense((TimeSenseRequest) request.Data[0]);
                            break;
                        case SenseRequest.StatusSense:
                            var start = (request.Data[0] & 0xF0) >> 4;
                            var length = request.Data[0] & 0x0F;
                            response = StatusSense(start, length);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;

                default:
                    response = new NakCommandBlock(NakCommandBlock.Nak.UndefinedError);
                    break;
            }

            return response;
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseCueUpWithData(byte[] data)
        {
            var handler = CueUpWithData;
            handler?.Invoke(this, new TimeCodeEventArgs(new TimeCode(data)));
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseInDataPreset(byte[] data)
        {
            var handler = InDataPreset;
            handler?.Invoke(this, new TimeCodeEventArgs(new TimeCode(data)));
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseOutDataPreset(byte[] data)
        {
            var handler = OutDataPreset;
            handler?.Invoke(this, new TimeCodeEventArgs(new TimeCode(data)));
        }

        /// <summary>
        ///     The eject.
        /// </summary>
        protected virtual void RaiseEject()
        {
            var handler = Eject;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The fast fwd.
        /// </summary>
        protected virtual void RaiseFastFwd()
        {
            var handler = FastFwd;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The jog fwd.
        /// </summary>
        protected virtual void RaiseJogFwd()
        {
            var handler = JogFwd;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The jog rev.
        /// </summary>
        protected virtual void RaiseJogRev()
        {
            var handler = JogRev;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The local disable.
        /// </summary>
        protected virtual void RaiseLocalDisable()
        {
            var handler = LocalDisable;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The local enable.
        /// </summary>
        protected virtual void RaiseLocalEnable()
        {
            var handler = LocalEnable;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The play.
        /// </summary>
        protected virtual void RaisePlay()
        {
            var handler = Play;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The preroll.
        /// </summary>
        protected virtual void RaisePreroll()
        {
            var handler = Preroll;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The record.
        /// </summary>
        protected virtual void RaiseRecord()
        {
            var handler = Record;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The rewind.
        /// </summary>
        protected virtual void RaiseRewind()
        {
            var handler = Rewind;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The shuttle fwd.
        /// </summary>
        protected virtual void RaiseShuttleFwd()
        {
            var handler = ShuttleFwd;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The shuttle rev.
        /// </summary>
        protected virtual void RaiseShuttleRev()
        {
            var handler = ShuttleRev;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The standby off.
        /// </summary>
        protected virtual void RaiseStandbyOff()
        {
            var handler = StandbyOff;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The standby on.
        /// </summary>
        protected virtual void RaiseStandbyOn()
        {
            var handler = StandbyOn;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The stop.
        /// </summary>
        protected virtual void RaiseStop()
        {
            var handler = Stop;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The var fwd.
        /// </summary>
        protected virtual void RaiseVarFwd()
        {
            var handler = VarFwd;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The var rev.
        /// </summary>
        protected virtual void RaiseVarRev()
        {
            var handler = VarRev;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     SenseRequest.
        /// </summary>
        /// <param name="start">
        ///     The start.
        /// </param>
        /// <param name="length">
        ///     The length.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected abstract CommandBlock? StatusSense(int start, int length);

        /// <summary>
        ///     SenseRequest
        /// </summary>
        /// <param name="timeSenseRequest">
        ///     The time sense request.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected abstract CommandBlock? TcGenSense(TimeSenseRequest timeSenseRequest);


        /// <summary>
        /// 
        /// </summary>
        private static readonly EventWaitHandle DiscoveryWaitHandle = new AutoResetEvent(false);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] Discover()
        {
            var activePorts = new List<string>();

            foreach (var serialPort in SerialPort.GetPortNames())
            {
                using var bvw75 = new Sony9PinMaster();
                if (!bvw75.Open(serialPort))
                {
                    continue;
                }

                try
                {
                    bvw75.DeviceType += (o, args) => DiscoveryWaitHandle.Set();
                    if (DiscoveryWaitHandle.WaitOne(500))
                        activePorts.Add(serialPort);
                }
                finally
                {
                    bvw75.Close();
                }
            }

            return activePorts.ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sender is not SerialPort serialPort)
                throw new ArgumentNullException(nameof(sender));

            // Sometimes the serialPort reports data, even if the port was closed.
            if (!serialPort.IsOpen)
                return;

            // Only deal with the arrival of chars, ignore all other events
            if (e.EventType != SerialData.Chars)
                return;

            var count = serialPort.BytesToRead;
            var buffer = new byte[count];
            var bytesRead = serialPort.Read(buffer, 0, count);

            Debug.Assert(bytesRead == count, "Bytes read not count");

            // Add the read bytes to the end of the buffer
            InputBuffer.AddRange(buffer);

            try
            {
                var commandBlock = new CommandBlock();
                while (null != commandBlock)
                {
                    if (!CommandBlock.TryParse(InputBuffer, out commandBlock))
                        continue;

                    InputBuffer.Clear();

                    Serial.DiscardInBuffer();
                    Serial.DiscardOutBuffer();

                    if (commandBlock == null) continue;
                    var response = ProcessRequest(commandBlock);

                    Send(response);
                }
            }
            catch (ArgumentException)
            {
            }
        }

        #endregion
    }
}