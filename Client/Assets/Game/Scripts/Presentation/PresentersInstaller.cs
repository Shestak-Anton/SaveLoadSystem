using System;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class PresentersInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IControlsPresenter>().To<ControlsPresenter>().AsSingle();
        }
    }
}