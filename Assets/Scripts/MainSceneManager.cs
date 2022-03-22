using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class MainSceneManager : MonoBehaviour
    {
        private IGameServiceble GameService { get; set; }
        private ISettingServiceble SettingsService { get; set; }
        private ISceneble SceneService { get; set; }
        private Canvas MainSceneCanvas { get; set; }
        private MainMenuController MainMenu { get; set; }
        private GameSettingsContoller GameSettingsMenu { get; set; }

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

        private void OnEnable()
        {
            MainMenu.mainMenuEvent += OnMainMenuButtonClickHandler;
            GameSettingsMenu.backEvent += OnSettingsMenuBackButtonClickHandler;
        }

        private void OnDisable()
        {
            MainMenu.mainMenuEvent -= OnMainMenuButtonClickHandler;
            GameSettingsMenu.backEvent -= OnSettingsMenuBackButtonClickHandler;
        }

        /// <summary>Настраивает RectTransform меню относительно родителя и активирует/деактивирует меню.</summary>
        /// <param name="menuObject">GameObject для получения RectTransform.</param>
        /// <param name="isActive">Флаг активации.</param>
        private void SetMenuSetting(GameObject menuObject, bool isActive)
        {
            RectTransform rect = menuObject.transform.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.localPosition = Vector3.zero;
                rect.localScale = new Vector3(0.4f, 0.8f, 0.4f);
            }

            menuObject.SetActive(isActive);
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

        private void OnSettingsMenuBackButtonClickHandler(object _, GameSettings currentSettings)
        {
            SettingsService.SetGameSettings(currentSettings);

            GameSettingsMenu.gameObject.SetActive(false);
            MainMenu.gameObject.SetActive(true);
        }
    }
}