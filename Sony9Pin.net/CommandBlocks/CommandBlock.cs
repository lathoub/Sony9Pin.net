using System.Diagnostics;
using System.Text;
using Sony9Pin.net.CommandBlocks.Return;

namespace Sony9Pin.net.CommandBlocks
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("Cmd1DataCount = {Cmd1DataCount}, Cmd2 = {Cmd2}, Data = {Data}, CheckSum = {CheckSum}")]
    public class CommandBlock : IComparable, IEquatable<CommandBlock>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBlock" /> class.
        /// </summary>
        public CommandBlock()
            : this(0x00, 0x00, Array.Empty<byte>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBlock" /> class.
        /// </summary>
        /// <param name="cmd1DataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        /// <param name="data">
        /// </param>
        public CommandBlock(byte cmd1DataCount, byte cmd2, byte[] data)
        {
            Cmd1DataCount = cmd1DataCount;
            Cmd2 = cmd2;
            Data = data;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBlock" /> class.
        /// </summary>
        /// <param name="cmd1">
        /// </param>
        /// <param name="dataCount">
        /// </param>
        /// <param name="cmd2">
        /// </param>
        /// <param name="data">
        /// </param>
        public CommandBlock(Cmd1 cmd1, int dataCount, byte cmd2, byte[] data)
            : this((byte)(((byte)cmd1 << 4) + dataCount), cmd2, data)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBlock" /> class.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        public CommandBlock(byte[] buffer)
        {
            Debug.Assert(buffer.Length >= 3, "Buffer is not long enough");

            Cmd1DataCount = buffer[0];
            Cmd2 = buffer[1];

            var dataCount = Cmd1DataCount & 0x0F;
            Data = new byte[dataCount];
            Buffer.BlockCopy(buffer, 2, Data, 0, dataCount);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the Cmd1 (Leaving DataCount untouched)
        /// </summary>
        public Cmd1 Cmd1
        {
            get => GetCmd1(Cmd1DataCount);

            set => Cmd1DataCount = (byte)((Cmd1DataCount & 0x0F) | ((int)value << 4));
        }

        /// <summary>
        ///     Gets or sets the cmd 1 data count.
        /// </summary>
        public byte Cmd1DataCount { get; set; }

        /// <summary>
        ///     Gets or sets the cmd 2.
        /// </summary>
        public byte Cmd2 { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        ///     Gets or sets the DataCount (leaving Cmd1 untouched).
        /// </summary>
        public int DataCount
        {
            get => (Cmd1DataCount & 0x0F);

            set => Cmd1DataCount = (byte)((Cmd1DataCount & 0xF0) | value);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="cmd1DataCount"></param>
        /// <returns></returns>
        public static Cmd1 GetCmd1(byte cmd1DataCount)
        {
            return (Cmd1)((cmd1DataCount & 0xF0) >> 4);
        }

        /// <summary>
        /// Converts the byte array representation of a commandblock. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A byte array containing a CommandBlock to convert.</param>
        /// <param name="result">When this method returns, contains the CommandBlock value equivalent of 
        /// the buffer contained in s, if the conversion succeeded, or zero if the conversion failed.
        ///  The conversion fails if the s parameter is null, is not of the correct format. This 
        /// parameter is passed uninitialized.</param>
        /// <returns>true if s was converted successfully; otherwise, false</returns>
        public static bool TryParse(List<byte> s, out CommandBlock? result )
        {
            result = null;

            try
            {
                // We need a minimum 3 bytes to work with (Cmd1Datacount, Cmd2 and Checksum)
                // See Cmd1 Block Format of the Sony 9-Pin remote protocol.
                if (s.Count < 3)
                {
                    return false; // No, wait for more Data to come in.
                }

                //
                //Protocol.Cmd1 cmd1;
                //if (!Protocol.Cmd1.TryParse(s[0].ToString(), out cmd1))
                //    throw new ArgumentException();

                var cmd1 = GetCmd1(s[0]);

                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Parse content of the incoming Buffer
                // CMD-1 is the upper 4 bits, DATA COUNT is the lower 4.

                var dataCount = GetDataCount(s[0]);
                var commandBlockLength = 2 + dataCount + 1;
                // 2 = sizeof(Cmd1DataCount) + sizeof(Cmd2) + dataCount + sizeof(checksum)

                // Do we have enough bytes to work with?
                // (3 = length of cmd1datacount(1) + length of Cmd2(1) + length of checksum(1))
                if (s.Count < commandBlockLength)
                {
                    return false; // No, wait for more Data to come in.
                }

                var cmd2 = s[1];

                var data = new byte[dataCount];
                for (var i = 0; i < dataCount; i++) data[i] = s[2 + i];

                // Check if the checksum is correct
                var checkSum = s[commandBlockLength - 1];

                // Checksum = lower eight bits of the sum of the bytes in the Cmd1 block
                // (exclusing the checksum itself)
                var calculatedCheckSum = 0;
                for (var i = 0; i < commandBlockLength - 1; i++)
                {
                    calculatedCheckSum += s[i];
                }
                calculatedCheckSum &= 0xFF;

                if (calculatedCheckSum != checkSum)
                {
                    result = new NakCommandBlock(NakCommandBlock.Nak.ChecksumError);
                    return true;
                }

                result = new CommandBlock(cmd1, dataCount, cmd2, data);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cmd1DataCount"></param>
        /// <returns></returns>
        public static int GetDataCount(byte cmd1DataCount)
        {
            return (cmd1DataCount & 0x0F);
        }

        /// <summary>
        /// </summary>
        /// <param name="cmd1"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public static byte ToCmd1DataCount(Cmd1 cmd1, int dataCount)
        {
            var cmd1DataCount = (byte)0x00;

            if (dataCount > 0x0F)
                throw new ArgumentOutOfRangeException(nameof(dataCount), $"Maximum length of dataCount is 15, supplied dataCount is {dataCount}");

            cmd1DataCount = (byte)((cmd1DataCount & 0x0F) | ((int)cmd1 << 4));
            cmd1DataCount = (byte)((cmd1DataCount & 0xF0) | dataCount);

            return cmd1DataCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object? obj)
        {
            var other = obj as CommandBlock;
            Debug.Assert(other != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CommandBlock? other)
        {
            if (null == other) return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>byte[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public byte[] ToBytes()
        {
            var buffer = new byte[3 + Data.Length];
            buffer[0] = Cmd1DataCount;
            buffer[1] = Cmd2;
            Buffer.BlockCopy(Data, 0, buffer, 2, Data.Length);

            buffer[2 + Data.Length] = CheckSum;

            return buffer;
        }

        /// <summary>
        ///     Assemble a friendly string
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            switch (Cmd1)
            {
                case Cmd1.SystemControl:
                    switch ((SystemControl.SystemControl)Cmd2)
                    {
                        case SystemControl.SystemControl.LocalDisable:
                            sb.Append("LocalDisable");
                            break;
                        case SystemControl.SystemControl.DeviceTypeRequest:
                            sb.Append("DeviceTypeRequest");
                            break;
                        case SystemControl.SystemControl.LocalEnable:
                            sb.Append("LocalEnable");
                            break;
                    }
                    break;

                case Cmd1.Return:
                    switch ((Return.Return)Cmd2)
                    {
                        case Return.Return.Ack:
                            sb.Append("Ack");
                            break;
                        case Return.Return.DeviceType:
                            sb.Append("DeviceType");
                            break;
                        case Return.Return.Nak:
                            sb.Append("Nak");
                            break;
                    }
                    break;

                case Cmd1.TransportControl:
                    switch ((TransportControl.TransportControl)Cmd2)
                    {
                        case TransportControl.TransportControl.Stop:
                            sb.Append("Stop");
                            break;
                        case TransportControl.TransportControl.Play:
                            sb.Append("Play");
                            break;
                        case TransportControl.TransportControl.Record:
                            sb.Append("Record");
                            break;
                        case TransportControl.TransportControl.StandbyOff:
                            sb.Append("StandbyOff");
                            break;
                        case TransportControl.TransportControl.StandbyOn:
                            sb.Append("StandbyOn");
                            break;
                        case TransportControl.TransportControl.Eject:
                            sb.Append("Eject");
                            break;
                        case TransportControl.TransportControl.FastFwd:
                            sb.Append("FastFwd");
                            break;
                        case TransportControl.TransportControl.JogFwd:
                            sb.Append("JogFwd");
                            break;
                        case TransportControl.TransportControl.VarFwd:
                            sb.Append("VarFwd");
                            break;
                        case TransportControl.TransportControl.ShuttleFwd:
                            sb.Append("ShuttleFwd");
                            break;
                        case TransportControl.TransportControl.Rewind:
                            sb.Append("Rewind");
                            break;
                        case TransportControl.TransportControl.JogRev:
                            sb.Append("JogRev");
                            break;
                        case TransportControl.TransportControl.VarRev:
                            sb.Append("VarRev");
                            break;
                        case TransportControl.TransportControl.ShuttleRev:
                            sb.Append("ShuttleRev");
                            break;
                        case TransportControl.TransportControl.Preroll:
                            sb.Append("Preroll");
                            break;
                        case TransportControl.TransportControl.CueUpWithData:
                            sb.Append("CueUpWithData");
                            break;
                    }
                    break;

                case Cmd1.PresetSelectControl:
                    break;

                case Cmd1.SenseRequest:
                    switch ((SenseRequest.SenseRequest)Cmd2)
                    {
                        case SenseRequest.SenseRequest.TcGenSense:
                            sb.Append("TcGenSense");
                            break;
                        case SenseRequest.SenseRequest.CurrentTimeSense:
                            sb.Append("CurrentTimeSense");
                            break;
                        case SenseRequest.SenseRequest.StatusSense:
                            sb.Append("StatusSense");
                            break;
                    }
                    break;

                case Cmd1.SenseReturn:
                    sb.Append("SenseReturn");
                    break;

                default:
                    sb.Append("Unknown");
                     break;
           }

            return sb.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calculate the checksum based on the current fields
        /// </summary>
        /// <returns>
        ///     The <see cref="byte" />.
        /// </returns>
        public byte CheckSum
        {
            get
            {
                var checksum = Cmd1DataCount;
                checksum += Cmd2;
                foreach (var b in Data)
                {
                    checksum += b;
                }

                //        checksum &= 0xFF;

                return checksum;
            }
        }

        #endregion
    }
}