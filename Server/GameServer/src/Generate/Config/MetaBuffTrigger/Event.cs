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
public sealed partial class Event :  Base 
{
    public Event(ByteBuf _buf)  : base(_buf) 
    {
        BuffEvent = (BuffEvent)_buf.ReadInt();
        PostInit();
    }

    public static Event DeserializeEvent(ByteBuf _buf)
    {
        return new MetaBuffTrigger.Event(_buf);
    }

    /// <summary>
    /// buff的生命周期
    /// </summary>
    public BuffEvent BuffEvent { get; private set; }

    public const int __ID__ = 410156428;
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
        + "BuffEvent:" + BuffEvent + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}