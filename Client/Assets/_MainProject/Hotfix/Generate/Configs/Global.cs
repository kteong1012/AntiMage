//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace Cfg
{
public sealed partial class Global :  Bright.Config.BeanBase 
{
    public Global(ByteBuf _buf) 
    {
        LoginServerAddress = _buf.ReadString();
        PostInit();
    }

    public static Global DeserializeGlobal(ByteBuf _buf)
    {
        return new Global(_buf);
    }

    public string LoginServerAddress { get; private set; }

    public const int __ID__ = 2135814083;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "LoginServerAddress:" + LoginServerAddress + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}