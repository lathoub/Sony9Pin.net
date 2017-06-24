namespace Acme.Sony9Pin.CommandBlocks.TransportControl
{
    /// <summary>
    /// 
    /// </summary>
    public class Rewind : CommandBlock
    {
        /// <summary>
        ///     When it receives this command, the _slave runs in reverse at maximum speed: on the DVR2000, this is 50xplay speed.
        /// </summary>
        public Rewind()
        {
            Cmd1 = Cmd1.TransportControl;
            Cmd2 = (byte)TransportControl.Rewind;
        }
    }
}
