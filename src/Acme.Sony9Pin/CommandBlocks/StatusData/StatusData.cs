using System;
using System.Diagnostics;
using System.Text;

namespace Acme.Sony9Pin.CommandBlocks.StatusData
{
    /// <summary>
    ///     The status data.
    /// </summary>
    public class StatusData : IComparable, IEquatable<StatusData>
    {
        #region Constants

        /// <summary>
        ///     The a 1_.
        /// </summary>
        internal const byte a1_ = 1 << 0;

        /// <summary>
        ///     The a 2_.
        /// </summary>
        internal const byte a2_ = 1 << 1;

        /// <summary>
        ///     The a 3_.
        /// </summary>
        internal const byte a3_ = 1 << 2;

        /// <summary>
        ///     The a 4_.
        /// </summary>
        internal const byte a4_ = 1 << 3;

        /// <summary>
        ///     The a in_.
        /// </summary>
        internal const byte aIn_ = 1 << 2;

        /// <summary>
        ///     The a out_.
        /// </summary>
        internal const byte aOut_ = 1 << 3;

        /// <summary>
        ///     The assemble_.
        /// </summary>
        internal const byte assemble_ = 1 << 5;

        /// <summary>
        ///     The aud split_.
        /// </summary>
        internal const byte audSplit_ = 1 << 5;

        /// <summary>
        ///     The auto edit_.
        /// </summary>
        internal const byte autoEdit_ = 1 << 2;

        /// <summary>
        ///     The auto mode_.
        /// </summary>
        internal const byte autoMode_ = 1 << 7;

        /// <summary>
        ///     The busy_.
        /// </summary>
        internal const byte busy_ = 1 << 7;

        /// <summary>
        ///     The buzzer_.
        /// </summary>
        internal const byte buzzer_ = 1 << 7;

        /// <summary>
        ///     The cf lock_.
        /// </summary>
        internal const byte cfLock_ = 1 << 3;

        /// <summary>
        ///     The cf mode_.
        /// </summary>
        internal const byte cfMode_ = 1 << 4;

        /// <summary>
        ///     The cue up_.
        /// </summary>
        internal const byte cueUp_ = 1 << 0;

        /// <summary>
        ///     The edit_.
        /// </summary>
        internal const byte edit_ = 1 << 4;

        /// <summary>
        ///     The eject_.
        /// </summary>
        internal const byte eject_ = 1 << 4;

        /// <summary>
        ///     The eot_.
        /// </summary>
        internal const byte eot_ = 1 << 4;

        /// <summary>
        ///     The fast fwd_.
        /// </summary>
        internal const byte fastFwd_ = 1 << 2;

        /// <summary>
        ///     The fnc abort_.
        /// </summary>
        internal const byte fncAbort_ = 1 << 7;

        /// <summary>
        ///     The freeze on_.
        /// </summary>
        internal const byte freezeOn_ = 1 << 6;

        /// <summary>
        ///     The full e e_.
        /// </summary>
        internal const byte fullEE_ = 1 << 6;

        /// <summary>
        ///     The hard error_.
        /// </summary>
        internal const byte hardError_ = 1 << 2;

        /// <summary>
        ///     The in out_.
        /// </summary>
        internal const byte inOut_ = 1 << 0;

        /// <summary>
        ///     The in_.
        /// </summary>
        internal const byte in_ = 1 << 0;

        // Data 5
        /// <summary>
        ///     The insert_.
        /// </summary>
        internal const byte insert_ = 1 << 6;

        /// <summary>
        ///     The jog_.
        /// </summary>
        internal const byte jog_ = 1 << 4;

        /// <summary>
        ///     The lamp fwd_.
        /// </summary>
        internal const byte lampFwd_ = 1 << 5;

        /// <summary>
        ///     The lamp rev_.
        /// </summary>
        internal const byte lampRev_ = 1 << 4;

        /// <summary>
        ///     The lamp still_.
        /// </summary>
        internal const byte lampStill_ = 1 << 6;

        /// <summary>
        ///     The local_.
        /// </summary>
        internal const byte local_ = 1 << 0;

        /// <summary>
        ///     The lost lock_.
        /// </summary>
        internal const byte lostLock_ = 1 << 6;

        /// <summary>
        ///     The near eot_.
        /// </summary>
        internal const byte nearEot_ = 1 << 5;

        /// <summary>
        ///     The out_.
        /// </summary>
        internal const byte out_ = 1 << 1;

        /// <summary>
        ///     The play_.
        /// </summary>
        internal const byte play_ = 1 << 0;

        /// <summary>
        ///     The preroll_.
        /// </summary>
        internal const byte preroll_ = 1 << 0;

        /// <summary>
        ///     The preview in preset_.
        /// </summary>
        internal const byte previewInPreset_ = 1 << 0;

        /// <summary>
        ///     The preview out preset_.
        /// </summary>
        internal const byte previewOutPreset_ = 1 << 1;

        /// <summary>
        ///     The preview_.
        /// </summary>
        internal const byte preview_ = 1 << 1;

        /// <summary>
        ///     The rec inhib_.
        /// </summary>
        internal const byte recInhib_ = 1 << 0;

        /// <summary>
        ///     The record_.
        /// </summary>
        internal const byte record_ = 1 << 1;

        /// <summary>
        ///     The review_.
        /// </summary>
        internal const byte review_ = 1 << 3;

        /// <summary>
        ///     The rewind_.
        /// </summary>
        internal const byte rewind_ = 1 << 3;

        /// <summary>
        ///     The select e e_.
        /// </summary>
        internal const byte selectEE_ = 1 << 7;

        /// <summary>
        ///     The servo lock_.
        /// </summary>
        internal const byte servoLock_ = 1 << 7;

        /// <summary>
        ///     The servo ref missing_.
        /// </summary>
        internal const byte servoRefMissing_ = 1 << 4;

        /// <summary>
        ///     The shuttle_.
        /// </summary>
        internal const byte shuttle_ = 1 << 5;

        /// <summary>
        ///     The spot erase_.
        /// </summary>
        internal const byte spotErase_ = 1 << 2;

        /// <summary>
        ///     The srch led 1_.
        /// </summary>
        internal const byte srchLed1_ = 1 << 0;

        /// <summary>
        ///     The srch led 2_.
        /// </summary>
        internal const byte srchLed2_ = 1 << 1;

        /// <summary>
        ///     The srch led 4_.
        /// </summary>
        internal const byte srchLed4_ = 1 << 2;

        /// <summary>
        ///     The srch led 8_.
        /// </summary>
        internal const byte srchLed8_ = 1 << 3;

        /// <summary>
        ///     The standy_.
        /// </summary>
        internal const byte standy_ = 1 << 7;

        /// <summary>
        ///     The still_.
        /// </summary>
        internal const byte still_ = 1 << 1;

        /// <summary>
        ///     The stop_.
        /// </summary>
        internal const byte stop_ = 1 << 5;

        /// <summary>
        ///     The svo alarm_.
        /// </summary>
        internal const byte svoAlarm_ = 1 << 2;

        /// <summary>
        ///     The sync act_.
        /// </summary>
        internal const byte syncAct_ = 1 << 4;

        /// <summary>
        ///     The sys alarm_.
        /// </summary>
        internal const byte sysAlarm_ = 1 << 1;

        /// <summary>
        ///     The tape dir_.
        /// </summary>
        internal const byte tapeDir_ = 1 << 2;

        /// <summary>
        ///     The tape end_.
        /// </summary>
        internal const byte tapeEnd_ = 1 << 6;

        /// <summary>
        ///     The tape out_.
        /// </summary>
        internal const byte tapeOut_ = 1 << 5;

        /// <summary>
        ///     The tape top_.
        /// </summary>
        internal const byte tapeTop_ = 1 << 7;

        /// <summary>
        ///     The tension release_.
        /// </summary>
        internal const byte tensionRelease_ = 1 << 6;

        /// <summary>
        ///     The tso_.
        /// </summary>
        internal const byte tso_ = 1 << 6;

        /// <summary>
        ///     The var_.
        /// </summary>
        internal const byte var_ = 1 << 3;

        /// <summary>
        ///     The video_.
        /// </summary>
        internal const byte video_ = 1 << 4;

        #endregion

        #region Fields

        #endregion

        ///
        public byte[] Data { get; } = new byte[10];

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusData" /> class.
        ///     Constructor
        /// </summary>
        public StatusData()
        {
            Array.Clear(Data, 0, Data.Length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusData"/> class.
        ///     Copy Constructor
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public StatusData(byte[] data)
        {
            Buffer.BlockCopy(data, 0, Data, 0, data.Length);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool Assemble
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & assemble_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool AudSplit
        {
            get
            {
                lock (Data)
                {
                    return (Data[7] & audSplit_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool AutoEdit
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & autoEdit_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[4] |= autoEdit_;
                        }
                        else
                        {
                            Data[4] &= (byte)(~autoEdit_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high when auto mode is enable, low
        ///     when auto mode is disabled.
        /// </summary>
        public bool AutoMode
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & autoMode_) != 0;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // _data 0

        /// <summary>
        ///     This bit is high if the Video disk recorder can not accept
        ///     motion commands or other commands requiring
        ///     time consuming processing, low if those commands
        ///     can be accepted. Note that status commands
        ///     and other sensing commands must always be processed,
        ///     even if this bit is set high.
        /// </summary>
        public bool Busy
        {
            get
            {
                lock (Data)
                {
                    return (Data[0] & busy_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[0] |= busy_;
                        }
                        else
                        {
                            Data[0] &= (byte)(~busy_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Buzzer
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & buzzer_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool CfLock
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & cfLock_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is set high at the completion of a Play/Record
        ///     cue command or Preset In command. It is set
        ///     low on initialization, and any time the current time
        ///     code position changes due to a motion command,
        ///     etc.
        /// </summary>
        public bool CueUp
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & cueUp_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= cueUp_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~cueUp_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Edit
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & edit_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high while ejecting.
        /// </summary>
        public bool Eject
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & eject_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= eject_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~eject_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Eot
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & eot_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high when in fast forward mode, low for other modes.
        /// </summary>
        public bool FastFwd
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & fastFwd_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= fastFwd_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~fastFwd_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool FncAbort
        {
            get
            {
                lock (Data)
                {
                    return (Data[9] & fncAbort_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool FreezeOn
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & freezeOn_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool FullEE
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & fullEE_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high if a hardware error or other unrecoverable
        ///     error is encountered, low if no such error has occurred.
        /// </summary>
        public bool HardError
        {
            get
            {
                lock (Data)
                {
                    return (Data[0] & hardError_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[0] |= hardError_;
                        }
                        else
                        {
                            Data[0] &= (byte)(~hardError_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high if the auto mode in preset references
        ///     a valid Id and contains a valid time code position.
        ///     Otherwise, it is set low.
        /// </summary>
        public bool In
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & in_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool InOut
        {
            get
            {
                lock (Data)
                {
                    return (Data[7] & inOut_) != 0;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // _data 5

        /// <summary>
        /// </summary>
        public bool Insert
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & insert_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high when in Jog mode, low for other modes.
        /// </summary>
        public bool Jog
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & jog_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= jog_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~jog_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool LampFwd
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & lampFwd_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool LampRev
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & lampRev_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool LampStill
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & lampStill_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high if the Video disk recorder is under Local
        ///     control, low if remote control is enabled. Status
        ///     sense is always available to the controlling device,
        ///     even under Local control.
        /// </summary>
        public bool Local
        {
            get
            {
                lock (Data)
                {
                    return (Data[0] & local_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[0] |= local_;
                        }
                        else
                        {
                            Data[0] &= (byte)(~local_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool LostLock
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & lostLock_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool NearEot
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & nearEot_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is set high if the auto mode out preset contains
        ///     a valid time code position. Otherwise, it is set
        ///     low.
        /// </summary>
        public bool Out
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & out_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high when in Play or Record mode, low for other modes.
        /// </summary>
        public bool Play
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & play_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= play_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~play_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high while cueing (following the issuing
        ///     of a Record/Play cue command or Preset In
        ///     command), and is set low high at completion.
        /// </summary>
        public bool Preroll
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & preroll_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[4] |= preroll_;
                        }
                        else
                        {
                            Data[4] &= (byte)(~preroll_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Preview
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & preview_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[4] |= preview_;
                        }
                        else
                        {
                            Data[4] &= (byte)(~preview_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high if the auto mode Preview in preset
        ///     references a valid Id and contains a valid time code
        ///     position. Otherwise, it is set low. The auto mode
        ///     Preview presets will become invalid, and this bit will
        ///     be set low, when the auto mode Preview presets are
        ///     shifted to the auto mode presets as part of the auto
        ///     Play processing. This will be the means by which a
        ///     controlling device can determine when it can set new
        ///     values for the auto mode Preview presets.
        /// </summary>
        public bool PreviewInPreset
        {
            get
            {
                lock (Data)
                {
                    return (Data[9] & previewInPreset_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is set high if the auto mode Preview out preset
        ///     contains a valid time code position. Otherwise, it
        ///     is set low. The auto mode Preview presets will become
        ///     invalid, and this bit will be set low, when the
        ///     auto mode Preview presets are shifted to the auto
        ///     mode presets as part of the auto Play processing.
        ///     This will be the means by which a controlling device
        ///     can determine when it can set new values for the
        ///     auto mode Preview presets.
        /// </summary>
        public bool PreviewOutPreset
        {
            get
            {
                lock (Data)
                {
                    return (Data[9] & previewOutPreset_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool RecInhib
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & recInhib_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high when in Record mode, low for other modes.
        /// </summary>
        public bool Record
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & record_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= record_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~record_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Review
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & review_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[4] |= review_;
                        }
                        else
                        {
                            Data[4] &= (byte)(~review_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is high when in Rewind mode, low for other modes.
        /// </summary>
        public bool Rewind
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & rewind_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= rewind_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~rewind_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool SelectEE
        {
            get
            {
                lock (Data)
                {
                    return (Data[4] & selectEE_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is high when a Play or Record mode is active,
        ///     low for other modes such as when the output is not
        ///     genlocked to the reference. While servo lock probably
        ///     has no meaning for a Video disk recorder, this
        ///     status bit is provided in order to maintain compatibility
        ///     with existing Video cassette recorder implementations.
        /// </summary>
        public bool ServoLock
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & servoLock_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= servoLock_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~servoLock_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool ServoRefMissing
        {
            get
            {
                lock (Data)
                {
                    return (Data[0] & servoRefMissing_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[0] |= servoRefMissing_;
                        }
                        else
                        {
                            Data[0] &= (byte)(~servoRefMissing_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is high when in Shuttle mode, low for other modes.
        /// </summary>
        public bool Shuttle
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & shuttle_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= shuttle_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~shuttle_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool SpotErase
        {
            get
            {
                lock (Data)
                {
                    return (Data[7] & spotErase_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SrchLed1
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & srchLed1_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SrchLed2
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & srchLed2_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SrchLed4
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & srchLed4_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SrchLed8
        {
            get
            {
                lock (Data)
                {
                    return (Data[6] & srchLed8_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is always high.
        /// </summary>
        public bool Standby
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & standy_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= standy_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~standy_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is high when time code is not changing
        ///     (stopped, Still, zero speed Shuttle, Jog, or variable
        ///     Play, etc.).
        /// </summary>
        public bool Still
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & still_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= still_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~still_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is high when in Stop mode, low for other modes.
        /// </summary>
        public bool Stop
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & stop_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= stop_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~stop_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool SvoAlarm
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & svoAlarm_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SyncAct
        {
            get
            {
                lock (Data)
                {
                    return (Data[7] & syncAct_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool SysAlarm
        {
            get
            {
                lock (Data)
                {
                    return (Data[8] & sysAlarm_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is low when time code is increasing
        ///     (“moving” forward), high when time code is decreasing
        ///     (“moving” in reverse).
        /// </summary>
        public bool TapeDir
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & tapeDir_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= tapeDir_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~tapeDir_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high when the current time code position
        ///     has reached its maximum value and can not increase
        ///     (“move” forward). Once set, this bit will be
        ///     cleared (set low) by any movement command (Play,
        ///     Rewind, etc.), and will continue to be set low as long
        ///     as the current time code position can increase
        ///     (“move” forward). This bit is also used during recording
        ///     to indicate to the controlling device that
        ///     there is no more available storage in the Video disk
        ///     recorder.
        /// </summary>
        public bool TapeEnd
        {
            get
            {
                lock (Data)
                {
                    return (Data[13] & tapeEnd_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is always low.
        /// </summary>
        public bool TapeOut
        {
            get
            {
                lock (Data)
                {
                    return (Data[0] & tapeOut_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[0] |= tapeOut_;
                        }
                        else
                        {
                            Data[0] &= (byte)(~tapeOut_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is set high when the current time code position
        ///     has reached its minimum value and can not decrease
        ///     (“move” in reverse). Once set, this bit will be
        ///     cleared (set low) by any movement command (Play,
        ///     Rewind, etc.), and will continue to be set low as long
        ///     as the current time code position can decrease
        ///     (“move” in reverse).
        /// </summary>
        public bool TapeTop
        {
            get
            {
                lock (Data)
                {
                    return (Data[13] & tapeTop_) != 0;
                }
            }
        }

        /// <summary>
        ///     This bit is always low.
        /// </summary>
        public bool TensionRelease
        {
            get
            {
                lock (Data)
                {
                    return (Data[1] & tensionRelease_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[1] |= tensionRelease_;
                        }
                        else
                        {
                            Data[1] &= (byte)(~tensionRelease_);
                        }
                    }
            }
        }

        /// <summary>
        ///     This bit is high when in variable Play  mode, low for other modes.
        /// </summary>
        public bool Var
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & var_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= var_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~var_);
                        }
                    }
            }
        }

        /// <summary>
        /// </summary>
        public bool Video
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & video_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool a1
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & a1_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool a2
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & a2_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool a3
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & a3_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool a4
        {
            get
            {
                lock (Data)
                {
                    return (Data[5] & a4_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool aIn
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & aIn_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool aOut
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & aOut_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool cfMode
        {
            get
            {
                lock (Data)
                {
                    return (Data[3] & cfMode_) != 0;
                }
            }
        }

        /// <summary>
        /// </summary>
        public bool tso
        {
            get
            {
                lock (Data)
                {
                    return (Data[2] & tso_) != 0;
                }
            }

            set
            {
                lock (Data)
                    unchecked
                    {
                        if (value)
                        {
                            Data[2] |= tso_;
                        }
                        else
                        {
                            Data[2] &= (byte)(~tso_);
                        }
                    }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="start">
        /// </param>
        /// <param name="length">
        /// </param>
        public byte[] GetData(int start, int length)
        {
            if (start + start > Data.Length)
                throw new ArgumentOutOfRangeException();

            var data = new byte[length];
            Buffer.BlockCopy(Data, start, data, 0, length);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(StatusData other)
        {
            if (Data.Length != other?.Data.Length) 
                return false;
            for (var i = 0; i < Data.Length; i++) 
                if (Data[i] != other.Data[i]) 
                    return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var ee in Data)
            {
                sb.Append(Convert.ToString(ee, 2).PadLeft(8, '0'));
                sb.Append(" ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var other = obj as StatusData;
            Debug.Assert(other != null);

            throw new NotImplementedException();
        }

        #endregion

    }
}