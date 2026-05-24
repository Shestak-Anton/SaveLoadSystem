using Modules.Extensions;
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

        public override void InstallBindings()
        {
            Container.Install(_repositoryInstaller);
        }
    }
}