﻿using Nino.Serialization;
using System;
using System.Reflection;
using System.Text;

namespace PostMainland
{
    public class ProtocalHelper
    {
        public static CompressOption CompressOption = CompressOption.NoCompression;
        public static Encoding Encoding = Encoding.UTF8;
        public static byte[] SerializeProtocal<T>(T protocal) where T : IProtocal
        {
            byte[] buffer = Serializer.Serialize(protocal, Encoding, CompressOption);
            return buffer;
        }
        public static T DeserializeProtocal<T>(byte[] body) where T : IProtocal
        {
            T protocal = Deserializer.Deserialize<T>(body, Encoding, CompressOption);
            return protocal;
        }
        public static IProtocal DeserializeProtocal(Type type, byte[] body)
        {
            IProtocal protocal = Deserializer.Deserialize(type, body, Encoding, CompressOption) as IProtocal;
            return protocal;
        }
        public static uint GetProtocalId(IProtocal protocal)
        {
            ProtocalAttribute protocalAttr = protocal.GetType().GetCustomAttribute<ProtocalAttribute>();
            if (protocalAttr != null)
            {
                return protocalAttr.Id;
            }
            return 0;
        }
        public static ProtocalType GetProtocalType(IProtocal protocal)
        {
            return protocal switch
            {
                IRequest _ => ProtocalType.Request,
                IResponse _ => ProtocalType.Response,
                IProtocal _ => ProtocalType.Protocal,
                _ => ProtocalType.InValid
            };
        }
    }
}