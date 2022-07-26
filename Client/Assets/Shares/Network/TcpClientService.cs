﻿using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Config;
using TouchSocket.Core.Dependency;
using TouchSocket.Core.Pool;
using TouchSocket.Sockets;

namespace PostMainland
{
    public class TcpClientService : IDisposable
    {
        public TcpClient TcpClient { get; private set; }
        public bool IsConnected { get; private set; }
        private ObjectPool<RequestAwaiter> _awaiterPool = new ObjectPool<RequestAwaiter>();
        private List<RequestAwaiter> _awatingRequests = new List<RequestAwaiter>();
        private IProtocalManagerService _protocalManager;
        private int _rpciId = 0;
        public TcpClientService(IPHost remote)
        {
            TcpClient = new TcpClient();
            _protocalManager = Global.Container.Resolve<IProtocalManagerService>();
            TcpClient.Setup(new TouchSocketConfig()
                .SetRemoteIPHost(remote)
                .SetDataHandlingAdapter(() => new ProtocalRequestHeaderHandlingAdapter()));
            TcpClient.Received += OnReceived;
            TcpClient.Connected += (_, _) => IsConnected = true;
            TcpClient.Disconnected += (_, _) => IsConnected = false;
        }

        public async UniTask ConnectAsync()
        {
            await TcpClient.ConnectAsync().AsUniTask();
        }

        private void OnReceived(TcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            if (requestInfo is ProtocalRequest pr)
            {
                switch (pr.Type)
                {
                    case ProtocalType.Response:
                        Type type = _protocalManager.GetProtocalType(pr.Id);
                        IResponse response = ProtocalHelper.DeserializeProtocal(type, pr.Body) as IResponse;
                        var request = _awatingRequests.FirstOrDefault(x => x.RpcId == response.RpcId);
                        if (request != null)
                        {

                            ThreadSynchronizationContext.Instance.PostNext(() => request.SetCompleted(response));
                        }
                        break;
                    case ProtocalType.Request:
                    case ProtocalType.Protocal:
                    default:
                        var handler = _protocalManager.GetMessageHandler(pr.Id);
                        if (handler != null)
                        {
                            ThreadSynchronizationContext.Instance.PostNext(() => handler.Handle(new TcpC2SSession(client), pr.Body));
                        }
                        break;
                }
            }
        }
        public void Send<T>(TcpClient client, T protocal, bool check = false) where T : IProtocal
        {
            client.Send(ProtocalRequest.FromProtocal(protocal, check).ToBytes());
        }
        public async UniTask<TRes> Request<TRes, TReq>(TcpClient client, TReq request, bool check = false, float timeout = 2f)
              where TRes : IResponse
              where TReq : IRequest<TRes>
        {
            request.RpcId = ++_rpciId;
            Send(client, request, check);
            var awaiter = _awaiterPool.GetObject();
            awaiter.Init(request.RpcId, new UniTaskCompletionSource<IResponse>(), _awatingRequests);
            UniTask timeoutTask = UniTaskHelper.Wait((int)Math.Round(timeout * 1000));
            var (hasResult, result) = await UniTask.WhenAny(awaiter.TCS.Task, timeoutTask);
            if (!hasResult)
            {
                Log.Error("超时");
            }
            _awaiterPool.DestroyObject(awaiter);
            return (TRes)result;
        }

        public void Dispose()
        {
            TcpClient.Dispose();
        }
    }
}
