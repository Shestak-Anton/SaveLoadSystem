using System;
using Zenject;

namespace Game.Gameplay.SaveLoad
{
    [Serializable]
    public sealed class SerializationSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EntitySerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<CountdownSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<DestinationPointSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<HealthSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ProductOrderSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ResourceBagSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<TargetObjectSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<TeamSerializer>()
                .AsSingle();
        }
    }
}