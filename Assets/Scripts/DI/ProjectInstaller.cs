using Zenject;

namespace Arkanoid
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Game settings service
            Container.Bind<ISettingServicable>().To<SettingService>().AsSingle().NonLazy();

            // InputController
            Container.Bind<IInputServicable>().To<InputService>().AsSingle().NonLazy();

            // SceneController
            Container.Bind<ISceneble>().To<SceneController>().FromNew().AsSingle().NonLazy();
        }
    }
}