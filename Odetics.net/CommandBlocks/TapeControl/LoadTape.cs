using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.TapeControl
{
    /// <summary>
    /// Note: This command has been added to address video disk recorder specific requirements
    /// 
    /// This command immediately aborts auto play of the current video segment specified
    ///  by the current auto mode preset, and performs the processing associated with the 
    /// end of the current auto mode preset (see 40.41, Auto Mode On).
    /// </summary>
    public class LoadTape : CommandBlock
    {
        public LoadTape()
        {
            Cmd1 = Cmd1.TapeControl;
            Cmd2 = (byte) TapeControl.LoadTape;
        }
    }
}
