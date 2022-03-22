using Zenject;

namespace Arkanoid
{
    public class ArkanoidInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Game settings service
            Container.Bind<ISettingServicable>().To<SettingService>().AsSingle().NonLazy();

            // InputController
            Container.Bind<IInputable>().To<InputController>().AsSingle().NonLazy();

            // SceneController
            Container.Bind<ISceneble>().To<SceneController>().FromNew().AsSingle().NonLazy();
        }
    }
}