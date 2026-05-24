using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "PresenterInstaller",
        menuName = "Zenject/New PresenterInstaller"
    )]
    public class PresentersInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IControlsPresenter>().To<ControlsPresenter>().AsSingle();
        }
    }
}