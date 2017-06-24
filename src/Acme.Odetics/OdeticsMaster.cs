// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdeticsMaster.cs" company="Acme">
//   2013 Acme
// </copyright>
// <summary>
//   The odetics master.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Acme.Sony9Pin;

namespace Acme.Odetics
{
    using System;
    using System.Text;

    /// <summary>
    ///     The odetics master.
    /// </summary>
    public class OdeticsMaster : Sony9PinMaster
    {
        /// <summary>
        /// Handy static method to convert a string to a 8 byte Clip Id Array
        /// </summary>
        /// <param name="name">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static byte[] StringTo8ByteId(string name)
        {
            var clipId = new byte[8];

            // convert the Clip string to a (padded) byte array
            var temp = new ASCIIEncoding().GetBytes(name);
            int t, len = Math.Min(temp.Length, clipId.Length);
            for (t = 0; t < len; t++)
            {
                // copy clip characters to byte[]
                clipId[t] = temp[t];
            }

            for (; t < clipId.Length; t++)
            {
                // padding 0-bytes
                clipId[t] = 0;
            }

            return clipId;
        }

        public static string ByteIdToString(byte[] id)
        {
            return new ASCIIEncoding().GetString(id).TrimEnd('\0');
        }
    }
}
