using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class MainSceneManager : MonoBehaviour
    {
        private ISceneble SceneController { get; set; }
        private Canvas MainSceneCanvas { get; set; }
        private MainMenuController MainMenu { get; set; }
        private GameSettingsContoller GameSettingsMenu { get; set; }

        [Inject]
        public void Construct(ISceneble sceneController, Canvas canvas, MainMenuController mainMenu, GameSettingsContoller gameSettingsContoller)
        {
            SceneController = sceneController;
            MainSceneCanvas = canvas;
            MainMenu = mainMenu;
            GameSettingsMenu = gameSettingsContoller;

            MainMenu.transform.parent = MainSceneCanvas.transform;
            GameSettingsMenu.transform.parent = MainSceneCanvas.transform;

            SetMenuSetting(GameSettingsMenu.gameObject, false);
            SetMenuSetting(MainMenu.gameObject, true);
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
    }
}