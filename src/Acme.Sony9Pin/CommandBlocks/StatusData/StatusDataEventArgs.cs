using System;

namespace Acme.Sony9Pin.CommandBlocks.StatusData
{
    /// <summary>
    ///     The status data event args.
    /// </summary>
    public class StatusDataEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///     The status data.
        /// </summary>
        public readonly StatusData StatusData;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusDataEventArgs"/> class.
        /// </summary>
        /// <param name="statusData">
        /// The status data.
        /// </param>
        public StatusDataEventArgs(StatusData statusData)
        {
            StatusData = statusData;
        }

        #endregion
    }
}