namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class EditOn : CommandBlock
    {
        /// <summary>
        ///     This command is used to actually initiate recording. When the device is playing, and the Edit presets set by the 4X
        ///     30 Edit Preset command are in place, the preset channels will enter Record a fixed delay after this command is
        ///     received. The _slave will enter Edit Rec mode at this point. It takes the _slave 5 frames to enter Edit Rec after
        ///     receiving this command.
        /// </summary>
        public EditOn()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.EditOn;
        }
    }
}