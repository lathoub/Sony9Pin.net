using System;
using System.Diagnostics;
using Acme.Sony9Pin;
using Acme.Sony9Pin.CommandBlocks;

namespace Acme.Odetics.CommandBlocks.PresetSelectControl
{
    /// <summary>
    /// This command will set the ID and/or the time code position for the auto mode in preset. The
    /// data provided for this command is variable. When an In Preset command is issued the VDR 
    /// should cue to this position. While this cue operation is in progress, the Preroll status bit 
    /// should be set. (see status byte 4 description) When cueing is finished, and the spot is found,
    /// the Preroll bit should be cleared and the Cue Complete bit should be set as well as Preset In.
    /// If the spot is not found, the VDR should clear the Preroll bit, Cue Complete, and In Preset 
    /// status bits. It should also set the Stop status bit. This will indicate that the position was 
    /// not found. Also the Out Preset value will be cleared.
    /// 
    /// Note: There is an implied order for Preset and Preview In and Out point, It is Preset In followed
    /// by Preset out then Preview In followed by Preview Out.
    ///
    /// </summary>
    public class InPreset : CommandBlock
    {
        /// <summary>
        ///     This command can be issued with no parameters. If zero bytes are specified,
        ///     the current time code position will be set to "00:00:00:00" and the currently
        ///     loaded Id will be used.
        /// </summary>
        public InPreset()
        {
            Cmd1 = Cmd1.PresetSelectControl;
            Cmd2 = (byte)Sony9Pin.CommandBlocks.PresetSelectControl.PresetSelectControl.InDataPreset;
        }

        /// <summary>
        /// This command can be issued with a single time code position parameter. If four
        ///  bytes of data are specified, the time code position for the auto mode in preset 
        /// will be set to the specified time code position within the currently loaded ID.
        /// </summary>
        /// <param name="tc"></param>
        public InPreset(TimeCode tc)
        {
            var data = tc.ToBinaryCodedDecimal();

            Cmd1DataCount = ToCmd1DataCount(Cmd1.PresetSelectControl, data.Length);
            Cmd2 = (byte)Sony9Pin.CommandBlocks.PresetSelectControl.PresetSelectControl.InDataPreset;
            Data = data;
        }

        /// <summary>
        /// This command can be issued with a single ID parameter. If eight bytes of data are
        /// specified, the ID for the auto mode in preset will be set to the specified ID, and
        /// the time code position for the auto mode in preset will be set to 00:00:00:00 If the 
        /// ID specified does not currently exist in the video disk recorder, the auto mode in 
        /// preset will become invalid for subsequent auto play operations.
        /// </summary>
        /// <param name="id"></param>
        public InPreset(byte[] id)
        {
            Debug.Assert(id != null);
            if (id.Length != 8)
                throw new ArgumentException("id needs to be exactly 8 bytes long.");

            Cmd1DataCount = ToCmd1DataCount(Cmd1.PresetSelectControl, id.Length);
            Cmd2 = (byte)Sony9Pin.CommandBlocks.PresetSelectControl.PresetSelectControl.InDataPreset;
            Data = id;
        }

        /// <summary>
        /// This command can be issued with two parameters indicating the time code position and ID. 
        /// If twelve bytes of data are specified, the first four bytes correspond to the time code 
        /// position and the next eight bytes correspond to the ID. When this command is issued, the
        /// ID for the auto mode in preset will be set to the specified ID, and the time code 
        /// position for the auto mode in preset will be set to the specified time code position.
        /// If the ID specified does not currently exist in the video disk recorder, the auto mode in
        /// preset will become invalid for subsequent auto play.
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="id"></param>
        public InPreset(TimeCode tc, byte[] id)
        {
            Debug.Assert(id != null);
            if (id.Length != 8)
                throw new ArgumentException("id needs to be exactly 8 bytes long.");

            var buffer = new byte[13];

            Buffer.BlockCopy(id, 0, buffer, 0, id.Length);
            Buffer.BlockCopy(tc.ToBinaryCodedDecimal(), 0, buffer, id.Length, 4);

            Cmd1DataCount = ToCmd1DataCount(Cmd1.PresetSelectControl, buffer.Length);
            Cmd2 = (byte)Sony9Pin.CommandBlocks.PresetSelectControl.PresetSelectControl.InDataPreset;
            Data = buffer;
        }

    }
}
