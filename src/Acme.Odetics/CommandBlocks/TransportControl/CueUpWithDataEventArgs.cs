// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeCodeEventArgs.cs" company="Acme">
//   2014 Acme
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Acme.Odetics.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class CueUpWithDataEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public int DataCount;

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CueUpWithDataEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="dataCount">
        /// The memory.
        /// </param>
        /// <param name="data">
        /// The memory.
        /// </param>
        public CueUpWithDataEventArgs(int dataCount, byte[] data)
        {
            Debug.Assert(dataCount == data.Length, "Length mismatch");
            DataCount = dataCount;

            Data = new byte[DataCount];
            Buffer.BlockCopy(data, 0, Data, 0, data.Length);
        }

        #endregion
    }
}