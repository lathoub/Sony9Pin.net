﻿using Sony9Pin.net;
using Sony9Pin.net.CommandBlocks;

namespace Odetics.net.CommandBlocks.xxxControl
{
    public class EraseSegment : CommandBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTC"></param>
        /// <param name="endTC"></param>
        public EraseSegment(TimeCode startTC, TimeCode endTC)
        {
            Cmd1 = Cmd1.xxxRequest;
            Cmd2 = (byte)xxxRequest.EraseSegment;
        }
    }
}