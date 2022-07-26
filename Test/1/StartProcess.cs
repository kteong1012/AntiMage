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

public sealed partial class StartProcess :  Bright.Config.BeanBase 
{
    public StartProcess(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["server_type"].IsString) { throw new SerializationException(); }  ServerType = _json["server_type"]; }
        { if(!_json["host"].IsString) { throw new SerializationException(); }  Host = _json["host"]; }
        { if(!_json["port"].IsNumber) { throw new SerializationException(); }  Port = _json["port"]; }
        { if(!_json["is_game_server"].IsBoolean) { throw new SerializationException(); }  IsGameServer = _json["is_game_server"]; }
        { if(!_json["database_name"].IsString) { throw new SerializationException(); }  DatabaseName = _json["database_name"]; }
        PostInit();
    }

    public StartProcess(int id, string server_type, string host, int port, bool is_game_server, string database_name ) 
    {
        this.Id = id;
        this.ServerType = server_type;
        this.Host = host;
        this.Port = port;
        this.IsGameServer = is_game_server;
        this.DatabaseName = database_name;
        PostInit();
    }

    public static StartProcess DeserializeStartProcess(JSONNode _json)
    {
        return new StartProcess(_json);
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 服务器类型
    /// </summary>
    public string ServerType { get; private set; }
    /// <summary>
    /// 监听地址
    /// </summary>
    public string Host { get; private set; }
    /// <summary>
    /// 监听端口
    /// </summary>
    public int Port { get; private set; }
    /// <summary>
    /// 是否是业务服务器
    /// </summary>
    public bool IsGameServer { get; private set; }
    /// <summary>
    /// 数据库名
    /// </summary>
    public string DatabaseName { get; private set; }

    public const int __ID__ = -279645235;
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
        + "Id:" + Id + ","
        + "ServerType:" + ServerType + ","
        + "Host:" + Host + ","
        + "Port:" + Port + ","
        + "IsGameServer:" + IsGameServer + ","
        + "DatabaseName:" + DatabaseName + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
