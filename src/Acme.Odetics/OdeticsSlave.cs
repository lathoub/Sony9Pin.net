// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdeticsSlave.cs" company="Acme">
//   2013 Acme
// </copyright>
// <summary>
//   The odetics slave.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Acme.Odetics.CommandBlocks.PresetSelectControl;
using Acme.Odetics.CommandBlocks.TransportControl;
using Acme.Sony9Pin;
using Acme.Sony9Pin.CommandBlocks;
using Acme.Sony9Pin.CommandBlocks.PresetSelectControl;
using Acme.Sony9Pin.CommandBlocks.Return;
using Acme.Sony9Pin.CommandBlocks.TransportControl;

namespace Acme.Odetics
{
    /// <summary>
    ///     The odetics slave.
    /// </summary>
    public abstract class OdeticsSlave : Sony9PinSlave
    {
        /// <summary>
        /// 
        /// </summary>
        public new event EventHandler<CueUpWithDataEventArgs> CueUpWithData;

        /// <summary>
        /// 
        /// </summary>
        public new event EventHandler<InDataPresetEventArgs> InDataPreset;

        /// <summary>
        /// 
        /// </summary>
        public new event EventHandler<OutDataPresetEventArgs> OutDataPreset;

        /// <summary>
        /// 
        /// </summary>
        protected OdeticsSlave()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        protected OdeticsSlave(byte[] deviceType)
        {
            DeviceType = deviceType;
        }

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler EraseID;

        /// <summary>
        /// 
        /// </summary>
   //     public event EventHandler AutoSkip;

        /// <summary>
        /// 
        /// </summary>
 //       public event EventHandler DeviceIDRequest;

        /// <summary>
        /// 
        /// </summary>
 //       public event EventHandler EraseSegment;

        /// <summary>
        /// 
        /// </summary>
//        public event EventHandler IDStatusRequest;

        /// <summary>
        /// 
        /// </summary>
 //       public event EventHandler ListFirstID;

        /// <summary>
        /// 
        /// </summary>
//        public event EventHandler ListNextID;

        /// <summary>
        /// 
        /// </summary>
//        public event EventHandler LongestContiguousStorageRequest;

        /// <summary>
        /// 
        /// </summary>
//        public event EventHandler SetDeviceID;

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="request">
        ///     The request.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandBlock" />.
        /// </returns>
        protected override CommandBlock ProcessRequest(CommandBlock request)
        {
            CommandBlock response = new AckCommandBlock();

            var handled = false;

            switch (request.Cmd1)
            {
                case Cmd1.SystemControl:
                     break;

                case Cmd1.TransportControl:
                    switch ((TransportControl)request.Cmd2)
                    {
                        case TransportControl.CueUpWithData:
                            switch (request.DataCount)
                            {
                                case 0x0:
                                    RaiseCueUpWithData(request.DataCount, request.Data);
                                    handled = true;
                                    break;
                                case 0x8:
                                    RaiseCueUpWithData(request.DataCount, request.Data);
                                    handled = true;
                                    break;
                                case 0xC:
                                    RaiseCueUpWithData(request.DataCount, request.Data);
                                    handled = true;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            break;
                        //case TransportControl.InPreset:
                        //    switch (request.DataCount)
                        //    {
                        //        case 0x8:
                        //            RaiseInPreset(request.DataCount, request.Data);
                        //            handled = true;
                        //            break;
                        //    }
                        //    break;
                    }
                    break;

                case Cmd1.PresetSelectControl:
                    switch ((PresetSelectControl)request.Cmd2)
                    {
                        case PresetSelectControl.InDataPreset:
                            RaiseInDataPreset(request.DataCount, request.Data);
                            handled = true;
                            break;
                        case PresetSelectControl.OutDataPreset:
                            RaiseOutDataPreset(request.DataCount, request.Data);
                            handled = true;
                            break;
                    }
                    break;

                case Cmd1.SenseRequest:
                     break;

                case Cmd1.SenseReturn:
                    break;
                case Cmd1.Return:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return !handled ? base.ProcessRequest(request) : response;
        }

        // TODO: Raise handlers for the other events

        /// <summary>
        /// </summary>
        protected virtual void RaiseEraseID()
        {
            var handler = EraseID;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="dataCount">
        ///     data.
        /// </param>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseCueUpWithData(int dataCount, byte[] data)
        {
            var handler = CueUpWithData;
            handler?.Invoke(this, new CueUpWithDataEventArgs(dataCount, data));
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="dataCount">
        ///     data.
        /// </param>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseInDataPreset(int dataCount, byte[] data)
        {
            var handler = InDataPreset;
            handler?.Invoke(this, new InDataPresetEventArgs(dataCount, data));
        }

        /// <summary>
        ///     The cue up with data.
        /// </summary>
        /// <param name="dataCount">
        ///     data.
        /// </param>
        /// <param name="data">
        ///     data.
        /// </param>
        protected virtual void RaiseOutDataPreset(int dataCount, byte[] data)
        {
            var handler = OutDataPreset;
            handler?.Invoke(this, new OutDataPresetEventArgs(dataCount, data));
        }

    }
}