﻿using PostMainland;
using System.Collections.Generic;
using TouchSocket.Core.Log;

namespace ProtocalDefine
{
    public class CSProtocal
    {
    }

    [ProtocalDefine]
    public class S2C_Notify : Protocal
    {
        public string Message { get; set; }
        public LogType MessageType { get; set; }
    }
    [ProtocalDefine]
    public class C2L_Login : Request<L2C_Login>
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public int SectionId { get; set; }
    }
    [ProtocalDefine]
    public class L2C_Login : Response
    {
        public string Name { get; set; }
    }
}