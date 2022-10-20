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

public sealed partial class Database :  Bright.Config.BeanBase 
{
    public Database(JSONNode _json) 
    {
        { if(!_json["name"].IsString) { throw new SerializationException(); }  Name = _json["name"]; }
        { if(!_json["connect_string"].IsString) { throw new SerializationException(); }  ConnectString = _json["connect_string"]; }
        PostInit();
    }

    public Database(string name, string connect_string ) 
    {
        this.Name = name;
        this.ConnectString = connect_string;
        PostInit();
    }

    public static Database DeserializeDatabase(JSONNode _json)
    {
        return new Database(_json);
    }

    /// <summary>
    /// 数据库名
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 连接地址
    /// </summary>
    public string ConnectString { get; private set; }

    public const int __ID__ = 1854109083;
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
        + "Name:" + Name + ","
        + "ConnectString:" + ConnectString + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}