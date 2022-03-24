using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class MainSceneManager : BaseManager
    {
        private Canvas MainSceneCanvas { get; set; }
        private MainMenuController MainMenu { get; set; }

        [Inject]
        public void ConstrustServices(IGameServiceble gameService, ISceneble sceneService, ISettingServiceble settingService)
        {
            GameService = gameService;
            SceneService = sceneService;
            SettingsService = settingService;
        }

        [Inject]
        public void ConstructObjects(Canvas canvas, MainMenuController mainMenu, GameSettingsContoller gameSettingsContoller)
        {
            MainSceneCanvas = canvas;
            MainMenu = mainMenu;
            GameSettingsMenu = gameSettingsContoller;

            MainMenu.transform.parent = MainSceneCanvas.transform;
            GameSettingsMenu.transform.parent = MainSceneCanvas.transform;

            SetMenuSetting(GameSettingsMenu.gameObject, false);
            SetMenuSetting(MainMenu.gameObject, true);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MainMenu.mainMenuEvent += OnMainMenuButtonClickHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainMenu.mainMenuEvent -= OnMainMenuButtonClickHandler;
        }

        private void OnMainMenuButtonClickHandler(object _, MainMenuCommand menuCommand)
        {
            switch (menuCommand)
            {
                case MainMenuCommand.NewGame:
                    Debug.Log("Начать новую игру");
                    SceneService.LoadScene(Scene.GameScene);
                    break;
                case MainMenuCommand.Settings:
                    Debug.Log("Переход в меню настроек");
                    MainMenu.gameObject.SetActive(false);
                    // Получить актуальные настройки и отдать на меню 
                    GameSettingsMenu.gameObject.SetActive(true);
                    GameSettingsMenu.UpdateSettings(SettingsService.GetGameSettings());
                    break;
                case MainMenuCommand.Exit:
                    GameService.ExitGame();
                    break;
            }
        }

        protected override void OnSettingsMenuBackButtonClickHandler(object sender, GameSettings currentSettings)
        {
            base.OnSettingsMenuBackButtonClickHandler(sender, currentSettings);
            MainMenu.gameObject.SetActive(true);
        }
    }
}