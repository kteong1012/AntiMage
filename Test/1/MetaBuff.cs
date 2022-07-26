//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace Cfg
{ 

public sealed partial class MetaBuff :  Bright.Config.BeanBase 
{
    public MetaBuff(JSONNode _json) 
    {
        { if(!_json["trigger"].IsObject) { throw new SerializationException(); }  Trigger = MetaBuffTrigger.Base.DeserializeBase(_json["trigger"]);  }
        { if(!_json["effect"].IsObject) { throw new SerializationException(); }  Effect = MetaBuffEffect.Base.DeserializeBase(_json["effect"]);  }
        PostInit();
    }

    public MetaBuff(MetaBuffTrigger.Base trigger, MetaBuffEffect.Base effect ) 
    {
        this.Trigger = trigger;
        this.Effect = effect;
        PostInit();
    }

    public static MetaBuff DeserializeMetaBuff(JSONNode _json)
    {
        return new MetaBuff(_json);
    }

    public MetaBuffTrigger.Base Trigger { get; private set; }
    public MetaBuffEffect.Base Effect { get; private set; }

    public const int __ID__ = -386354152;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        Trigger?.Resolve(_tables);
        Effect?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        Trigger?.TranslateText(translator);
        Effect?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Trigger:" + Trigger + ","
        + "Effect:" + Effect + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
