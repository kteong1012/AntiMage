using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostMainland
{

    public class StartUp : MonoBehaviour
    {
        void Awake()
        {
            AssemblyCollection.Ins.RegisterCollector(ProtocalCollector.Ins);
            AssemblyCollection.Ins.RegisterCollector(ProtocalHandlerCollector.Ins.LoadHandlers(this.GetType()));
        }

        public async void TestConnect()
        {
            await Network.Ins.Connect(ServerConfig.Host, ServerConfig.Port);
        }

        public async void TestSendMessage()
        {
            SC_LoginAck ack = await Network.Ins.RequestAsync<SC_LoginAck>(new CS_Login() { Account = "�˺�", Password = "password" });
            Debug.Log(ack.Name);
        }
        public async void TestDisconnect()
        {
            S2C_DisconnectAck ack = await Network.Ins.RequestAsync<S2C_DisconnectAck>(new C2S_Disconnect());
            if(ack.Error == 0)
            {
                Debug.Log(ack.Message);
                Network.Ins.Disconnect();
            }
            else
            {
                Debug.Log(ack.Message);
            }
        }
    }

}