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
        public void ConstructMenus(MainMenuController mainMenu, GameSettingsContoller gameMenu)
        {
            MainMenu = mainMenu;
            GameSettingsMenu = gameMenu;
        }

        [Inject]
        public void ConstructObjects(Canvas canvas)
        {
            MainSceneCanvas = canvas;

            MainMenu.transform.SetParent(MainSceneCanvas.transform, false);
            GameSettingsMenu.transform.SetParent(MainSceneCanvas.transform, false);

            SetMenuSetting(GameSettingsMenu.gameObject, false);
            SetMenuSetting(MainMenu.gameObject, true);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribing();
        }

        public void Subscribe()
        {
            MainMenu.mainMenuEvent += OnMainMenuButtonClickHandler;
            GameSettingsMenu.backEvent += OnSettingsMenuBackButtonClickHandler;
        }

        public void Unsubscribing()
        { 
            MainMenu.mainMenuEvent -= OnMainMenuButtonClickHandler;
            GameSettingsMenu.backEvent -= OnSettingsMenuBackButtonClickHandler;
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