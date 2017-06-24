namespace Acme.Sony9Pin
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public enum CgTimeRepresentation
    {
        /// <summary>
        /// 
        /// </summary>
        TimeCode,

        /// <summary>
        /// 
        /// </summary>
        Frames,

        /// <summary>
        /// 
        /// </summary>
        Fields,
    }

    /// <summary>
    /// 
    /// </summary>
    public class TimeCode : IComparable, IEquatable<TimeCode>
    {
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        public const long TicksPerHour = TicksPerMinute * MinutesPerHour;

        /// <summary>
        /// 
        /// </summary>
        public const long TicksPerMinute = TicksPerSecond * SecondsPerMinute;

        /// <summary>
        /// 
        /// </summary>
        public const long TicksPerSecond = TimeSpan.TicksPerSecond;

        /// <summary>
        /// 
        /// </summary>
        private const int FieldsPerFrame = 2;

        /// <summary>
        /// 
        /// </summary>
        private const int MinutesPerHour = 60;

        /// <summary>
        /// 
        /// </summary>
        private const int SecondsPerMinute = 60;

        #endregion

        #region Static Fields

        private static long _framesPerSecond = 25; // could also be 30 (NTSC)

        #endregion

        #region Fields

        /// <summary>
        ///     Internal data members
        /// </summary>
        private long _drilldownTotalTicks = -1;

        private int _fields;

        private int _hours;

        private bool _inToString;

        private int _minutes;

        private int _seconds;

        private int _ticks;

        /// <summary>
        ///     The one and only ticks storage data member
        /// </summary>
        private long _totalTicks;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Constructor from ticks
        /// </summary>
        /// <param name="ticks"></param>
        public TimeCode(long ticks)
        {
            _totalTicks = ticks;
        }

        /// <summary>
        ///     Constructor from TimeCode
        /// </summary>
        /// <param name="timecode"></param>
        public TimeCode(TimeCode timecode)
        {
            _totalTicks = timecode.TotalTicks;
        }

        /// <summary>
        ///     Constructor from TimeCode
        /// </summary>
        /// <param name="dateTime"></param>
        public TimeCode(DateTime dateTime)
            : this(TimeSpan.FromTicks(dateTime.Ticks).TotalSeconds)
        {
        }

        /// <summary>
        ///     Constructor from a TimeSpan
        /// </summary>
        /// <param name="ts"></param>
        public TimeCode(TimeSpan ts)
            : this(ts.Ticks)
        {
        }

        /// <summary>
        ///     Constructor from a total number of seconds
        /// </summary>
        /// <param name="totalSeconds"></param>
        public TimeCode(double totalSeconds)
        {
            TotalSeconds = totalSeconds;
        }

        /// <summary>
        ///     Constructor from hours, minutes, seconds, frames
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="frames"></param>
        public TimeCode(int hours, int minutes, int seconds, int frames)
        {
            SetData(hours, minutes, seconds, frames);
        }

        /// <summary>
        ///     Constructor from hours, minutes, seconds, frames, ticks
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="frames"></param>
        /// <param name="ticks"></param>
        public TimeCode(int hours, int minutes, int seconds, int frames, int ticks)
        {
            SetData(hours, minutes, seconds, frames, ticks);
        }

        /// <summary>
        /// Constructor from sony9pin protocol bytes
        /// 
        ///      data-1     data-2      data-3     data-4
        ///    | Frame | | Seconds | | minutes | | hours |
        ///    | 10  1 | |   10 1  | |   10 1  | | 10 1  |
        /// 
        /// auth - this is how time is represented in all 
        /// commands and responses using a time code. The 
        /// numbers indicate that the 10s value is stored 
        /// in the high nibble and the 1s value in the low 
        /// nibble. This is not to be confused with the 
        /// 80-bit SMPTE timecode which is present in the 
        /// analog timecode track on tape, or with the 
        /// VITC timecode. 
        ///
        /// </summary>
        /// <param name="tc"></param>
        public TimeCode(byte[] tc)
        {
            SetData(tc);
        }

        /// <summary>
        ///     Constructor from a hh:mm:ss:ff formatted string
        /// </summary>
        /// <param name="tc"></param>
        public TimeCode(string tc)
        {
            SetData(tc);
        }

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public TimeCode()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public static long FieldsPerSecond => _framesPerSecond * FieldsPerFrame;

        /// <summary>
        /// 
        /// </summary>
        public static long FramesPerSecond
        {
            get => _framesPerSecond;
            set => _framesPerSecond = value;
        }

        //////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Get the highest possible timecode
        /// </summary>
        public static TimeCode MaxValue => new TimeCode(long.MaxValue);

        /// <summary>
        ///     Get the lowest possible timecode
        /// </summary>
        public static TimeCode MinValue => new TimeCode(long.MinValue);

        /// <summary>
        ///     Get the 'now' absolute system time, in TimeCode form
        /// </summary>
        public static TimeCode Now => new TimeCode(DateTime.Now.Ticks);

        /// <summary>
        /// 
        /// </summary>
        public static long TicksPerField => TicksPerSecond / FieldsPerSecond;

        /// <summary>
        /// 
        /// </summary>
        public static long TicksPerFrame => TicksPerSecond / FramesPerSecond;

        /// <summary>
        ///     Get the Fields component
        /// </summary>
        public byte Fields
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return (byte)_fields;
                }
            }
        }

        /// <summary>
        ///     Get the frames component
        /// </summary>
        public byte Frames
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return (byte)(_fields / 2);
                }
            }
        }

        /// <summary>
        ///     Get the hours component
        /// </summary>
        public byte Hours
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return (byte)_hours;
                }
            }
        }

        /// <summary>
        ///     Get the minutes component
        /// </summary>
        public byte Minutes
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return (byte)_minutes;
                }
            }
        }

        /// <summary>
        ///     Get the seconds component
        /// </summary>
        public byte Seconds
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return (byte)_seconds;
                }
            }
        }

        /// <summary>
        ///     Get the ticks component
        /// </summary>
        public int Ticks
        {
            get
            {
                lock (this)
                {
                    DrillDown();
                    return _ticks;
                }
            }
        }

        /// <summary>
        ///     Get/set the TimeCode as a TimeSpan
        /// </summary>
        public TimeSpan TimeSpan
        {
            get => new TimeSpan(TotalTicks);
            set => TotalTicks = value.Ticks;
        }

        /// <summary>
        ///     get/set the total number of Fields (where fps = DefaultFramesPerSecond)
        /// </summary>
        public long TotalFields
        {
            get
            {
                lock (this)
                {
                    return _totalTicks / TicksPerField;
                }
            }

            set
            {
                lock (this)
                {
                    _totalTicks = value * TicksPerField;
                }
            }
        }

        /// <summary>
        ///     get/set the total number of frames (where fps = DefaultFramesPerSecond)
        /// </summary>
        public long TotalFrames
        {
            get
            {
                lock (this)
                {
                    return _totalTicks / TicksPerFrame;
                }
            }

            set
            {
                lock (this)
                {
                    _totalTicks = value * TicksPerFrame;
                }
            }
        }

        /// <summary>
        ///     Get/set the total number of hours (where fps = DefaultFramesPerSecond)
        /// </summary>
        public float TotalHours
        {
            get
            {
                lock (this)
                {
                    return (float)_totalTicks / TicksPerHour;
                }
            }
            set
            {
                lock (this)
                {
                    _totalTicks = (long)(value * TicksPerHour);
                }
            }
        }

        /// <summary>
        ///     Get/set the total number of minutes (where fps = DefaultFramesPerSecond)
        /// </summary>
        public float TotalMinutes
        {
            get
            {
                lock (this)
                {
                    return (float)_totalTicks / TicksPerMinute;
                }
            }
            set
            {
                lock (this)
                {
                    _totalTicks = (long)(value * TicksPerMinute);
                }
            }
        }

        /// <summary>
        ///     Get/set the total number of seconds
        /// </summary>
        public double TotalSeconds
        {
            get
            {
                lock (this)
                {
                    return (double)_totalTicks / TicksPerSecond;
                }
            }
            set
            {
                lock (this)
                {
                    _totalTicks = (long)(value * TicksPerSecond);
                }
            }
        }

        /// <summary>
        ///     get/set the total number of ticks (one tick = 100 nanoseconds)
        /// </summary>
        public long TotalTicks
        {
            get
            {
                lock (this)
                {
                    return _totalTicks;
                }
            }
            set
            {
                lock (this)
                {
                    _totalTicks = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Convert a byte array (in reversed order, as given by the BVW75 protocol) to a TimeCode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeCode FromBytes(byte[] value)
        {
            return new TimeCode(value);
        }

        /// <summary>
        ///     Convert a timecode TotalTicks value
        ///     to a timecode object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeCode FromTicks(long value)
        {
            return new TimeCode(value);
        }

        /// <summary>
        ///     Convert a timespan to a timecode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeCode FromTimeSpan(TimeSpan value)
        {
            return new TimeCode(value);
        }

        /// <summary>
        ///     Parse a timecode string into a timecode.
        ///     Can throw an exception upon failure.
        /// </summary>
        /// <param name="timeCodeString"></param>
        /// <returns></returns>
        public static TimeCode Parse(string timeCodeString)
        {
            if (timeCodeString == null)
            {
                throw new ArgumentNullException("timeCodeString");
            }

            var tc = new TimeCode();
            tc.SetData(timeCodeString);

            return tc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static long SnapToField(long ticks)
        {
            return (long)((((ticks + (0.5f * TicksPerField)) / TicksPerField)) * TicksPerField);
        }

        /// <summary>
        ///     Try to Parse a timecode string into a timecode.
        ///     returns true on success (giving 'result' the corresponding, valid timecode)
        ///     returns false on failure (giving 'result' a null value, because it is undefined)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string s, out TimeCode result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static TimeCode operator +(TimeCode tc1, TimeCode tc2)
        {
            return new TimeCode(tc1.TotalTicks + tc2.TotalTicks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static bool operator >(TimeCode tc1, TimeCode tc2)
        {
            return (tc1.TotalTicks > tc2.TotalTicks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static bool operator >=(TimeCode tc1, TimeCode tc2)
        {
            return (tc1.TotalTicks >= tc2.TotalTicks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static bool operator <(TimeCode tc1, TimeCode tc2)
        {
            return (tc1.TotalTicks < tc2.TotalTicks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static bool operator <=(TimeCode tc1, TimeCode tc2)
        {
            return (tc1.TotalTicks <= tc2.TotalTicks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tc1"></param>
        /// <param name="tc2"></param>
        /// <returns></returns>
        public static TimeCode operator -(TimeCode tc1, TimeCode tc2)
        {
            return new TimeCode(tc1.TotalTicks - tc2.TotalTicks);
        }

        /// <summary>
        ///     Increase the TimeCode by a number of frames
        /// </summary>
        /// <param name="fields"></param>
        public TimeCode AddFields(long fields)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + fields * TicksPerField);
            }
        }

        /// <summary>
        ///     Increase the TimeCode by a number of frames
        /// </summary>
        /// <param name="frames"></param>
        public TimeCode AddFrames(long frames)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + frames * TicksPerFrame);
            }
        }

        /// <summary>
        ///     Increase the TimeCode by a number of hours
        /// </summary>
        /// <param name="hours"></param>
        public TimeCode AddHours(long hours)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + hours * TicksPerHour);
            }
        }

        /// <summary>
        ///     Increase the TimeCode by a number of minutes
        /// </summary>
        /// <param name="minutes"></param>
        public TimeCode AddMinutes(long minutes)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + minutes * TicksPerMinute);
            }
        }

        /// <summary>
        ///     Increase the TimeCode by a number of seconds
        /// </summary>
        /// <param name="seconds"></param>
        public TimeCode AddSeconds(float seconds)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + (long)(seconds * TicksPerSecond));
            }
        }

        /// <summary>
        ///     Increase the TimeCode by a number of ticks
        /// </summary>
        /// <param name="ticks"></param>
        public TimeCode AddTicks(long ticks)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks + ticks);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int CompareTo(object b)
        {
            var other = b as TimeCode;
            Debug.Assert(other != null);
            lock (this)
            {
                return _totalTicks.CompareTo(other.TotalTicks);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimeCode other)
        {
            return other != null && (_totalTicks.Equals(other.TotalTicks));
        }

        //////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////
        /// <summary>
        ///     fetch the data, in an atomic thread-safe way
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="fields"></param>
        public void GetData(out int hours, out int minutes, out int seconds, out int fields)
        {
            lock (this)
            {
                DrillDown();
                hours = _hours;
                minutes = _minutes;
                seconds = _seconds;
                fields = _fields;
            }
        }

        /////////////////////////////////////////////////
        /// <summary>
        ///     fetch the data, in an atomic thread-safe way
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="fields"></param>
        /// <param name="ticks"></param>
        public void GetData(out int hours, out int minutes, out int seconds, out int fields, out int ticks)
        {
            lock (this)
            {
                DrillDown();
                hours = _hours;
                minutes = _minutes;
                seconds = _seconds;
                fields = _fields;
                ticks = _ticks;
            }
        }

        //////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Set the data from a byte array, in an atomic thread-safe way
        /// 
        ///      data-1     data-2      data-3     data-4
        ///    | Frame | | Seconds | | minutes | | hours |
        ///    | 10  1 | |   10 1  | |   10 1  | | 10 1  |
        /// 
        /// auth - this is how time is represented in all 
        /// commands and responses using a time code. The 
        /// numbers indicate that the 10s value is stored 
        /// in the high nibble and the 1s value in the low 
        /// nibble. This is not to be confused with the 
        /// 80-bit SMPTE timecode which is present in the 
        /// analog timecode track on tape, or with the 
        /// VITC timecode. 
        ///
        /// </summary>
        /// <param name="tc"></param>
        public void SetData(byte[] tc)
        {
            Debug.Assert(tc != null, "A timecode byte array should not be null");
            Debug.Assert(tc.Length == 4, "A timecode byte array should be exactly 4 bytes in the form [ff,ss,mm,hh]");

            // for the hours: set the first bit to 0 (field indication)
            var htc = (byte)(((byte)(tc[3] << 1) >> 1));

            var frames  = (((tc[0] >> 4) * 10) + (tc[0] & 0x0F));
            var seconds = (((tc[1] >> 4) * 10) + (tc[1] & 0x0F));
            var minutes = (((tc[2] >> 4) * 10) + (tc[2] & 0x0F));
            var hours   = (((  htc >> 4) * 10) + (htc   & 0x0F));

            var fields = frames * FieldsPerFrame;

            SetData(hours, minutes, seconds, fields);
        }

        /// <summary>
        ///     Set the data from the other timecode
        /// </summary>
        /// <param name="tc"></param>
        public void SetData(TimeCode tc)
        {
            TotalTicks = tc.TotalTicks;
        }

        /// <summary>
        ///     Set the data from a string
        ///     format: [-]hh:mm:ss:ff or [-]hh:mm:ss:mil (note: ',' instead of ':' is also allowed, as well as mixing ',' and ':')
        /// </summary>
        /// <param name="timeCodeString"></param>
        public void SetData(string timeCodeString)
        {
            if (timeCodeString == null)
            {
                throw new ArgumentNullException("timeCodeString");
            }

            if (string.IsNullOrEmpty(timeCodeString))
            {
                TotalTicks = 0;
                return;
            }

            var makeNegative = timeCodeString.StartsWith("-");

            // Remove all funny or unwanted characters
            timeCodeString = timeCodeString.Trim(new[] { '-', ' ', '\t' });

            var timeParts = timeCodeString.Split(new[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (timeParts.Length != 4)
            {
                // if a timecode is expressed as a single number, we parse it as an amount of seconds
                if (timeParts.Length == 1)
                {
                    TotalSeconds = Convert.ToSingle(timeCodeString);
                    //TotalSeconds =     Tools.FloatFromString(timeCodeString, true) * (makeNegative ? -1 : 1);
                    return;
                }

                // if that doesn't work, there is simply something wrong with the format
                throw new FormatException(
                    "A TimeCode should be in format [-]hh:mm:ss:ff or [-]hh:mm:ss:mil (note: ',' instead of ':' is also allowed, as well as mixing ',' and ':')");
            }

            var hours = Int32.Parse(timeParts[0]);
            var minutes = Int32.Parse(timeParts[1]);
            var seconds = Int32.Parse(timeParts[2]);

            int fields;
            switch (timeParts[3].Length)
            {
                case 2:
                    fields = Int32.Parse(timeParts[3]); // parse as fields
                    break;
                case 3:
                    fields = (int)(FieldsPerSecond * Int32.Parse(timeParts[3]) / 1000); // parse as milliseconds
                    break;
                default:
                    throw new FormatException(
                        "The last part of a timecode should have 2 digits for fields, or 3 digits for milliseconds");
            }

            SetData(hours, minutes, seconds, fields);

            if (makeNegative)
            {
                TotalTicks = -TotalTicks;
            }
        }

        /// <summary>
        ///     Set the data from separate values: hours, minutes, seconds, frames
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="fields"></param>
        public void SetData(int hours, int minutes, int seconds, int fields)
        {
            SetData(hours, minutes, seconds, fields, 0);
        }

        /// <summary>
        ///     Set the data from separate values: hours, minutes, seconds, frames, ticks
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="fields"></param>
        /// <param name="ticks"></param>
        public void SetData(int hours, int minutes, int seconds, int fields, int ticks)
        {
            TotalTicks = (hours   * TicksPerHour)
                            + (minutes * TicksPerMinute)
                            + (seconds * TicksPerSecond)
                            + (fields  * TicksPerField)
                            + (ticks);
        }

        /// <summary>
        ///     Set the data from a timespan
        /// </summary>
        /// <param name="timespan"></param>
        public void SetData(TimeSpan timespan)
        {
            TimeSpan = timespan;
        }

        //////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Decrease the TimeCode by a number of frames
        /// </summary>
        /// <param name="fields"></param>
        public TimeCode SubtractFields(long fields)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - fields * TicksPerField);
            }
        }

        /// <summary>
        ///     Decrease the TimeCode by a number of frames
        /// </summary>
        /// <param name="frames"></param>
        public TimeCode SubtractFrames(long frames)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - frames * TicksPerFrame);
            }
        }

        /////////////////////////////////////////////////

        /// <summary>
        ///     Decrease the TimeCode by a number of hours
        /// </summary>
        /// <param name="hours"></param>
        public TimeCode SubtractHours(long hours)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - hours * TicksPerHour);
            }
        }

        /// <summary>
        ///     Decrease the TimeCode by a number of minutes
        /// </summary>
        /// <param name="minutes"></param>
        public TimeCode SubtractMinutes(long minutes)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - minutes * TicksPerMinute);
            }
        }

        /// <summary>
        ///     Decrease the TimeCode by a number of seconds
        /// </summary>
        /// <param name="seconds"></param>
        public TimeCode SubtractSeconds(float seconds)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - (long)(seconds * TicksPerSecond));
            }
        }

        /// <summary>
        ///     Decrease the TimeCode by a number of ticks
        /// </summary>
        /// <param name="ticks"></param>
        public TimeCode SubtractTicks(long ticks)
        {
            lock (this)
            {
                return new TimeCode(_totalTicks - ticks);
            }
        }

        /// <summary>
        ///     Convert a TimeCode to a sequence of bytes (in reverse order, as expected by the BVW75 protocol)
        ///     in an atomic thread-safe way
        /// </summary>
        /// <returns></returns>
        public byte[] ToBinaryCodedDecimal()
        {
            byte frames;
            byte seconds;
            byte minutes;
            byte hours;

            lock (this)
            {
                // note: it is important to round the fields correctly by the remainder ticks, for exact representation in all cases!!!
                var roundedFields = Fields + (Ticks >= (TicksPerField >> 1) ? 1 : 0);
                frames = (byte)(roundedFields >> 1);
                seconds = Seconds;
                minutes = Minutes;
                hours = Hours;
            }
            return new[]
                       {
                           (byte)(((frames / 10) << 4) | (frames - (frames / 10 * 10))),
                           (byte)(((seconds / 10) << 4) | (seconds - (seconds / 10 * 10))),
                           (byte)(((minutes / 10) << 4) | (minutes - (minutes / 10 * 10))),
                           (byte)(((hours / 10) << 4) | (hours - (hours / 10 * 10)))
                       };
        }

        /// <summary>
        ///     Convert a TimeCode to a string of format hh:mm:ss:ff, in an atomic thread-safe way
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(CgTimeRepresentation.TimeCode);
        }

        /// <summary>
        /// </summary>
        /// <param name="representation"></param>
        /// <returns></returns>
        public string ToString(CgTimeRepresentation representation)
        {
            switch (representation)
            {
                case CgTimeRepresentation.TimeCode:
                    {
                        // note: it is important to round the fields correctly by the remainder ticks, for exact representation in all cases!!!
                        var roundedFields = (byte)(Fields + (Ticks >= (TicksPerField >> 1) ? 1 : 0));
                        if (_inToString)
                        {
                            return string.Format(
                                "{0:00}:{1:00}:{2:00}:{3:00}",
                                Hours,
                                Minutes,
                                Seconds,
                                roundedFields);
                        }
                        try
                        {
                            _inToString = true;
                            lock (this)
                            {
                                if (_totalTicks < 0
                                    && !(Hours == 0 && Minutes == 0 && Seconds == 0 && roundedFields == 0)) // make negative, but avoid negative zero
                                {
                                    var positive = new TimeCode(Math.Abs(_totalTicks));
                                    return ("-" + positive);
                                }
                                return string.Format(
                                    "{0:00}:{1:00}:{2:00}:{3:00}",
                                    Hours,
                                    Minutes,
                                    Seconds,
                                    roundedFields);
                            }
                        }
                        finally
                        {
                            _inToString = false;
                        }
                    }
                case CgTimeRepresentation.Frames:
                    return ((float)_totalTicks / TicksPerFrame).ToString("0.0", CultureInfo.InvariantCulture);
                case CgTimeRepresentation.Fields:
                    return (_totalTicks / TicksPerField).ToString(CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException("representation");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Drill down the hours, minutes, seconds and fields from the _totalTicks
        /// </summary>
        private void DrillDown()
        {
            // make sure we don't evaluate this when not needed
            if (_drilldownTotalTicks == _totalTicks)
            {
                return;
            }

            _drilldownTotalTicks = _totalTicks;

            var t = _totalTicks;
            _hours = (int)(t / TicksPerHour);
            t -= _hours * TicksPerHour;
            _minutes = (int)(t / TicksPerMinute);
            t -= _minutes * TicksPerMinute;
            _seconds = (int)(t / TicksPerSecond);
            t -= _seconds * TicksPerSecond;
            _fields = (int)(t / TicksPerField);
            t -= _fields * TicksPerField;
            _ticks = (int)(t);
        }

        #endregion
    }
}