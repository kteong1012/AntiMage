//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace Cfg.MetaBuffEffect
{
/// <summary>
/// Buff效果基类
/// </summary>
public abstract partial class Base :  Bright.Config.BeanBase 
{
    public Base(ByteBuf _buf) 
    {
        PostInit();
    }

    public static Base DeserializeBase(ByteBuf _buf)
    {
        switch (_buf.ReadInt())
        {
            case MetaBuffEffect.Damage.__ID__: return new MetaBuffEffect.Damage(_buf);
            case MetaBuffEffect.AttachState.__ID__: return new MetaBuffEffect.AttachState(_buf);
            case MetaBuffEffect.DetachState.__ID__: return new MetaBuffEffect.DetachState(_buf);
            default: throw new SerializationException();
        }
    }



    public virtual void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public virtual void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}