﻿using Cfg;
using Leopotam.EcsLite;
using TouchSocket.Core.Dependency;

namespace PostMainland
{
    public class WorldServerInitializer : GameServerInitializer
    {
        public override void Initialize(ServerType serverType, IContainer container)
        {
            base.Initialize(serverType, container);
            StartWorld();
        }

        private void StartWorld()
        {
            EcsWorld world = new EcsWorld();
            var gameEvent = new GameEvent();
            EcsSystems systems = new EcsSystems(world, gameEvent);
            systems.Add(new BuffComponentSystem())
                .Add(new BuffTickSystem())
                .Add(new BuffStatesComponentSystem())
                .Init();
            Program.UpdateEvent += () => systems.Run();
            int caster = world.NewEntity();
            int target = world.NewEntity();
            var unitPool = world.GetPool<Unit>();
            var buffsPool = world.GetPool<BuffComponent>();
            var numericPool = world.GetPool<NumericComponent>();
            var ap = world.GetPool<BuffAttachEvent>();
            ref var numeric = ref numericPool.Add(target);
            ref var unit = ref unitPool.Add(target);
            ref var uni2t = ref unitPool.Add(caster);
            world.Add<BuffStatesComponent>(target);
            gameEvent.Publish(new BuffAttachEvent() { CfgId = 10000101, CasterEntity = caster, TargetEntity = target });
            gameEvent.Publish(new BuffAttachEvent() { CfgId = 10000201, CasterEntity = caster, TargetEntity = target });
            UniTaskHelper.Wait(500, () =>
            {
                gameEvent.Publish(new BuffAttachEvent() { CfgId = 10000201, CasterEntity = caster, TargetEntity = target });
            }).Forget();
        }
    }
    public class GameServerInitializer : IGameServerInitializer
    {
        protected IContainer container;
        protected ServerType serverType;
        public virtual void Initialize(ServerType serverType, IContainer container)
        {
            this.container = container;
            this.serverType = serverType;

            AddAssemblyManager();
            AddProtocalManager();
            StartServerService(true);
        }

        public void AddAssemblyManager()
        {
            container.RegisterSingleton<IAssemblyManager, AssemblyManager>();
        }
        public void AddProtocalManager()
        {
            container.RegisterSingleton<IProtocalManagerService, ProtocalManager>();
        }
        public void StartServerService(bool connectDb = true)
        {
            var processCfg = TbStartProcess.Instance.GetOrDefault((int)serverType);
            if (processCfg != null)
            {
                if (processCfg.Host.NotEmpty() && processCfg.Port > 0)
                {
                    var service = container.Resolve<TcpServerService>();
                    service.Start(processCfg.Host, processCfg.Port);
                }
                if (connectDb && processCfg.DatabaseName.NotEmpty())
                {
                    var dbCfg = TbDatabase.Instance.GetOrDefault(processCfg.DatabaseName);
                    if (dbCfg != null)
                    {
                        var mongoDb = new MongoDBContext(dbCfg.ConnectString, dbCfg.Name);
                        container.RegisterSingleton<MongoDBContext>(mongoDb);
                    }
                }
            }
        }
    }
}