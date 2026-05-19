using System;
using Zenject;

namespace SaveSystem
{
    [Serializable]
    public sealed class SaveSystemInstaller : Installer
    {
        
        public override void InstallBindings()
        {
            Container.Bind<SaveManager>().AsSingle();
        }
    }
}