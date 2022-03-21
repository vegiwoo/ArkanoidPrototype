using Zenject;

namespace Arkanoid
{
    public class ArkanoidInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // InputController
            Container.Bind<IInputable>().To<InputController>().AsSingle().NonLazy();

            // SceneController
            Container.Bind<ISceneble>().To<SceneController>().FromNew().AsSingle().NonLazy();
        }
    }
}