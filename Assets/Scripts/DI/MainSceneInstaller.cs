using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainSceneManager mainSceneManager;
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenuController mainMenu;
        [SerializeField] private GameSettingsContoller gameSettingsMenu;

        public override void InstallBindings()
        {
            // MainSceneCanvas
            Canvas mainSceneCanvasInstance = Container.InstantiatePrefabForComponent<Canvas>(canvas.gameObject, canvas.transform.position, canvas.transform.rotation, null);
            mainSceneCanvasInstance.name = "MainSceneCanvas";
            Container.Bind<Canvas>().FromInstance(mainSceneCanvasInstance).AsSingle().NonLazy();

            // MainMenu
            MainMenuController mainMenuContoller = Container.InstantiatePrefabForComponent<MainMenuController>(mainMenu.gameObject, mainMenu.transform.position, mainMenu.transform.rotation, null);
            mainMenuContoller.name = "MainMenu";
            Container.Bind<MainMenuController>().FromInstance(mainMenuContoller).AsSingle().NonLazy();

            // SettingsMenu
            GameSettingsContoller gameSettingsContoller = Container.InstantiatePrefabForComponent<GameSettingsContoller>(gameSettingsMenu.gameObject, gameSettingsMenu.transform.position, gameSettingsMenu.transform.rotation, null);
            gameSettingsContoller.name = "SettingsMenu";
            Container.Bind<GameSettingsContoller>().FromInstance(gameSettingsContoller).AsSingle().NonLazy();

            // MainSceneManager
            Container.Bind<MainSceneManager>().FromInstance(mainSceneManager).AsSingle().NonLazy();
        }
    }
}