﻿using Cfg;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TouchSocket.Core.Dependency;
using TouchSocket.Core.Log;

namespace PostMainland
{
    public class Program
    {
        public static event Action UpdateEvent;
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ProcessLauncherOptions>(args).WithParsed(StartUp);
        }

        private static void StartUp(ProcessLauncherOptions options)
        {
            if (!Enum.TryParse(options.ServerType, out ServerType serverType))
            {
                Log.Error($"参数{options.ServerType}错误，没有这个服务器类型");
                return;
            }
            Log.SetLogs(new FileLogger() { LogType = (LogType)63 }
            , new ConsoleLogger() { LogType = (LogType)63 });
            SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

            new Luban();
            Global.Container = new Container();

            var pid = Process.GetCurrentProcess().Id;
            Log.Info($"{serverType}启动,pid:{pid}");
            try
            {
                if (serverType == ServerType.Main)
                {
                    StartAllServers(options.OneProcess);
                }
                if (options.IsGameServer)
                {
                    Server game = new Server(serverType);
                    game.Start();
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }



            while (true)
            {
                Thread.Sleep(1);
                Update();
                UpdateEvent?.Invoke();
                LateUpdate();
            }
        }
        private static void StartAllServers(bool oneProcess)
        {
            foreach (var serverInfo in TbStartProcess.Instance.DataList)
            {
                if (oneProcess)
                {
                    if (Enum.TryParse(serverInfo.ServerType, out ServerType serverType))
                    {
                        Server game = new Server(serverType);
                        game.Start();
                    }
                }
                else
                {
                    var launchers = new List<ProcessLauncher>();
                    ProcessLauncherOptions options = new ProcessLauncherOptions();
                    options.Host = serverInfo.Host;
                    options.Port = serverInfo.Port;
                    options.ServerType = serverInfo.ServerType;
                    options.IsGameServer = serverInfo.IsGameServer;

                    ProcessLauncher launcher = new ProcessLauncher(options);
                    launcher.Launch();
                    launchers.Add(launcher);

                    while (true)
                    {
                        Thread.Sleep(100);
                        launchers.ForEach(launcher => launcher.CheckAlive());
                    }
                }
            }
        }


        private static readonly ThreadSynchronizationContext _threadSynchronizationContext = ThreadSynchronizationContext.Instance;
        private static readonly TimeInfo _timeInfo = TimeInfo.Instance;

        public static void LateUpdate()
        {
        }

        public static void Update()
        {
            _timeInfo.Update();
            _threadSynchronizationContext.Update();
        }
    }
}