// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sony9Pin.cs" company="Acme">
//   2013 Acme
// </copyright>
// <summary>
//   The status data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;

using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Sony9Pin
{
    /// <summary>
    ///     The sony 9 pin.
    /// </summary>
    public abstract class Sony9Pin : IDisposable
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        protected static TraceSwitch TraceSwitch = new TraceSwitch("AppTraceLevel", null);

        /// <summary>
        ///     The _serial.
        /// </summary>
        protected readonly SerialPort Serial = new SerialPort();

        /// <summary>
        ///     The _input buffer.
        /// </summary>
        protected List<byte> InputBuffer = new List<byte>(32);

        /// <summary>
        ///     The _disposed.
        /// </summary>
        private bool _disposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Sony9Pin" /> class.
        ///     Constructor
        /// </summary>
        protected Sony9Pin()
        {
            Debug.Assert(null != Serial, "SerialPort is null");

            // According to the Sony9Pin/BVW protocol specification
            Serial.BaudRate = 38400;
            Serial.DataBits = 8;
            Serial.StopBits = StopBits.One;
            Serial.Parity = Parity.Odd;
            Serial.Handshake = Handshake.None;
            Serial.DtrEnable = true;
            Serial.RtsEnable = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the port name.
        /// </summary>
        public string PortName
        {
            get
            {
                Debug.Assert(null != Serial, "internal serial port is null");
                return Serial.PortName;
            }

            private set
            {
                Debug.Assert(null != Serial, "internal serial port is null");
                Serial.PortName = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Close the serial port
        /// </summary>
        public virtual void Close()
        {
            try
            {
                // If bytes are available in the input queue, then just read them here.
                // (but they will be lost)
                if (Serial.IsOpen)
                {
                    // Discards Data from the serial driver's receive/transmit Buffer.
                    Serial.DiscardInBuffer();
                    Serial.DiscardOutBuffer();

                    Serial.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
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
        public virtual bool Open(string portName)
        {
            PortName = portName;

            var portNames = SerialPort.GetPortNames();
            if (!portNames.Contains(PortName))
            {
                return false;
            }

            try
            {
                Debug.Assert(false == Serial.IsOpen, "internal serial port should not be open at this stage");

                Serial.Open();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                return false;
            }

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="buffer">
        ///     The response.
        /// </param>
        protected virtual void Send(byte[] buffer)
        {
            lock (this)
            {
                try
                {
                    if (Serial.IsOpen)
                    {
                        Serial.Write(buffer, 0, buffer.Length);
                    }
                }
                catch (IOException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="commandBlock">
        ///     The response.
        /// </param>
        protected virtual void Send(CommandBlock commandBlock)
        {
            if (commandBlock == null)
            {
                return;
            }

            //            Trace.WriteLineIf("Send from " + Name + " " + commandBlock.Cmd1);

            Send(commandBlock.ToBytes());
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        ///     true if managed resources should be disposed; otherwise, false.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                try
                {
                    if (disposing)
                    {
                        // Release the managed resources
                        Close();
                    }

                    // Release the native unmanaged resources here.
                    // NOP                        
                }
                finally
                {
                    // Call Dispose on the base class, if any
                }
            }
        }

        #endregion
    }
}