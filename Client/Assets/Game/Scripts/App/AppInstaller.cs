using Game.Gameplay;
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

        public override void InstallBindings()
        {
            Container
                .Install(_repositoryInstaller)
                .Install(_saveSystemInstaller)
                .Install(_presentersInstaller);
        }
    }
}