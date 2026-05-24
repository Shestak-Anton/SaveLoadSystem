using SaveSystem;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.SaveLoad
{
    [CreateAssetMenu(
        fileName = "SerializationSystemInstaller",
        menuName = "Zenject/New Serialization System Installer"
    )]
    public sealed class SerializationSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveManager>().AsSingle();
            
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
                .BindInterfacesAndSelfTo<TeamSerializer>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<TargetObjectSerializer>()
                .AsSingle();
        }
    }
}