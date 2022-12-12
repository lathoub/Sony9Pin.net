using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.rrrControl
{
    /// <summary>
    /// </summary>
    public class IDStatus : CommandBlock
    {
        /// <summary>
        /// This response is returned for the ID status request command (A8.18). 
        /// A single byte of data will be returned, indicating the status of the
        ///  specified ID. Additional ID status fields may be defined as required 
        /// by the video disk recorder implementation. This byte of data is a bit 
        /// field with the following format:
        /// 
        /// </summary>
        /// <param name="data"></param>
        public IDStatus(byte data)
        {
            Cmd1DataCount = ToCmd1DataCount(Cmd1.rrrReturn, 1);
            Cmd2 = (byte)rrrReturn.IDStatus;
            Data = new[] {data};
        }
    }
}
