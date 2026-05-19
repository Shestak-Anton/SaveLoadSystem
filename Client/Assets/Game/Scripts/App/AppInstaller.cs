using Game.Gameplay;
using Game.Gameplay.SaveLoad;
using Modules.Extensions;
using SaveSystem;
using UnityEngine;
using UseCases.Repository;
using Zenject;

namespace System.Runtime.CompilerServices.Domain.App
{
    [CreateAssetMenu(
        fileName = "AppInstaller",
        menuName = "RTS/New AppInstaller"
    )]
    public sealed class AppInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private RepositoryInstaller _repositoryInstaller;
        [SerializeField] private SaveSystemInstaller _saveSystemInstaller;
        [SerializeField] private PresentersInstaller _presentersInstaller;
        [SerializeField] private SerializationSystemInstaller _serializationSystemInstaller;

        public override void InstallBindings()
        {
            Container
                .Install(_repositoryInstaller)
                .Install(_saveSystemInstaller)
                .Install(_presentersInstaller)
                .Install(_serializationSystemInstaller);
        }
    }
}