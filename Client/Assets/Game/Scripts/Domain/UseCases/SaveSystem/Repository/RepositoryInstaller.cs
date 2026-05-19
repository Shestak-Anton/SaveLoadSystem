using System;
using UnityEngine;
using Zenject;

namespace UseCases.Repository
{
    [Serializable]
    public sealed class RepositoryInstaller : Installer
    {
        [SerializeField]
        private string _uri = "http://127.0.0.1:8888";

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<RemoteRepository>()
                .AsSingle()
                .WithArguments(_uri);
        }
    }
}