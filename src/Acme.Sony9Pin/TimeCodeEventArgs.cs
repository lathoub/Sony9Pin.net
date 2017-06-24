// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeCodeEventArgs.cs" company="Acme">
//   2014 Acme
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acme.Sony9Pin
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class TimeCodeEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public TimeCode TimeCode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="timeCode">
        /// The memory.
        /// </param>
        public TimeCodeEventArgs(TimeCode timeCode)
        {
            TimeCode = timeCode;
        }

        #endregion
    }
}