using UnityEngine;
using Zenject;

namespace Arkanoid
{
    /// <summary>Основной инсталлер для глобального контекста.</summary>
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameSettingsContoller settingsMenu;

        public override void InstallBindings()
        {
            // Game settings service
            Container.Bind<ISettingServiceble>().To<SettingService>().AsSingle().NonLazy();

            // Game service
            Container.Bind<IGameServiceble>().To<GameService>().AsSingle().NonLazy();

            // InputController
            Container.Bind<IInputServiceble>().To<InputService>().AsSingle().NonLazy();

            // SceneController
            Container.Bind<ISceneble>().To<SceneController>().FromNew().AsSingle().NonLazy();
        }
    }
}