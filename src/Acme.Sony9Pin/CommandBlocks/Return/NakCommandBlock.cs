using System.Collections;

namespace Acme.Sony9Pin.CommandBlocks.Return
{
    /// <summary>
    ///     The nak command block.
    /// </summary>
    public class NakCommandBlock : CommandBlock
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NakCommandBlock" /> class.
        /// </summary>
        /// <param name="error">
        ///     The error.
        /// </param>
        public NakCommandBlock(Nak error)
        {
            Cmd1 = Cmd1.Return;
            Cmd2 = (byte)Return.Nak;

            Data = new byte[1];
            DataCount = Data.Length;

            Data[0] = (byte)(Data[0] | (1 << (int)error));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public byte Error
        {
            get => Data[0];
            set => Data[0] &= value;
        }

        #region Enums

        /// <summary>
        /// 
        /// </summary>
        public enum Nak 
        {
            /// <summary>
            /// This bit is high when a communications time out error has occurred, otherwise it is low. 
            /// </summary>
            TimeOut = 7,
            /// This bit is high when a communications framing error has occurred, otherwise it is low. 
            FrameError = 6,
            /// .This bit is high when a communications overrun error has occurred, otherwise it is low. 
            OverrunError = 5,
            /// This bit is high when a communications parity error has occurred, otherwise it is low. 
            ParityError = 4,
            /// This bit is high when a communications checksum error has occurred, otherwise it is low. 
            ChecksumError = 2,
            /// This bit is high when an undefined command error has occurred, otherwise it is low.
            UndefinedError = 0,
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool IsChecksumError
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.ChecksumError));
            }
        }

        /// <summary>
        /// </summary>
        public bool IsFrameError
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.FrameError));
            }
        }

        /// <summary>
        /// </summary>
        public bool IsOverrunError
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.OverrunError));
            }
        }

        /// <summary>
        /// </summary>
        public bool IsParityError
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.ParityError));
            }
        }

        /// <summary>
        /// </summary>
        public bool IsTimeOut
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.TimeOut));
            }
        }

        /// <summary>
        /// </summary>
        public bool IsUndefinedError
        {
            get
            {
                var bits = new BitArray(new int[] { Data[0] });
                return (bits.Get((int)Nak.UndefinedError));
            }
        }

        #endregion
    }
}