﻿using TouchSocket.Core;

namespace PostMainland
{
    public class MessageIdGenerator
    {
        private static SnowflakeIDGenerator _generator = new SnowflakeIDGenerator(4);
        public static long NextID()
        {
            return _generator.NextID();
        }
    }
}