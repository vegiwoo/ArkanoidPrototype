using UnityEngine;
using Zenject;

namespace Arkanoid 
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PausedMenuController pausedMenu;
        [SerializeField] private GameSettingsContoller gameSettingsMenu;

        public BatSettings batSettings;

        public override void InstallBindings()
        {
            // BatSettings
            batSettings.batSpeed = 50.0f;
            Container.Bind<BatSettings>().FromInstance(batSettings).AsSingle().NonLazy();

            // Paused Menu
            PausedMenuController pausedMenuController = Container.InstantiatePrefabForComponent<PausedMenuController>(pausedMenu.gameObject, pausedMenu.transform.position, pausedMenu.transform.rotation, null);
            pausedMenuController.name = "PausedMenu";
            Container.Bind<PausedMenuController>().FromInstance(pausedMenuController).AsSingle().NonLazy();

            // SettingsMenu
            GameSettingsContoller gameSettingsContoller = Container.InstantiatePrefabForComponent<GameSettingsContoller>(gameSettingsMenu.gameObject, gameSettingsMenu.transform.position, gameSettingsMenu.transform.rotation, null);
            gameSettingsContoller.name = "SettingsMenu";
            Container.Bind<GameSettingsContoller>().FromInstance(gameSettingsContoller).AsSingle().NonLazy();

            // GameManager
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();
        }
    }
}