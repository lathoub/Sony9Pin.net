namespace Sony9Pin.net.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class EditOff : CommandBlock
    {
        /// <summary>
        ///     This command will Stop recording without affecting the state of motion of the device. Any channels in Record will
        ///     come out in response to this command, after 5 frames of delay. This command also clears the Manual Edit Record mode
        ///     and the Select EE mode.
        /// </summary>
        public EditOff()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.EditOff;
        }
    }
}