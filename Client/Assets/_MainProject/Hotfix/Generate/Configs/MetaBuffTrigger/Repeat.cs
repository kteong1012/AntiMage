//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace Cfg.MetaBuffTrigger
{
public sealed partial class Repeat :  Base 
{
    public Repeat(ByteBuf _buf)  : base(_buf) 
    {
        IntervalMs = _buf.ReadInt();
        PostInit();
    }

    public static Repeat DeserializeRepeat(ByteBuf _buf)
    {
        return new MetaBuffTrigger.Repeat(_buf);
    }

    /// <summary>
    /// 毫秒
    /// </summary>
    public int IntervalMs { get; private set; }

    public const int __ID__ = 186745065;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "IntervalMs:" + IntervalMs + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}