using UnityEngine;

namespace Arkanoid
{
    /// <summary>Реализует базовый функционал менеджера.</summary>
    public abstract class BaseManager : MonoBehaviour
    {
        #region Properties

        // Службы
        protected IGameServiceble GameService { get; set; }
        protected ISettingServiceble SettingsService { get; set; }
        protected ISceneble SceneService { get; set; }

        // Меню
        protected GameSettingsContoller GameSettingsMenu { get; set; }

        #endregion

        #region MonoBehaviour methods
        // ...
        #endregion

        #region Methods

        /// <summary>Настраивает RectTransform меню относительно родителя и активирует/деактивирует меню.</summary>
        /// <param name="menuObject">GameObject для получения RectTransform.</param>
        /// <param name="isActive">Флаг активации.</param>
        protected void SetMenuSetting(GameObject menuObject, bool isActive)
        {
            RectTransform rect = menuObject.transform.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.localPosition = Vector3.zero;
                rect.localScale = new Vector3(0.4f, 0.8f, 0.4f);
            }

            menuObject.SetActive(isActive);
        }

        /// <summary>Обрабатывает изменние настроек игры и передает их в SettingsService.</summary>
        /// <param name="_">Источник.</param>
        /// <param name="currentSettings">Текущие настройки игры.</param>
        protected virtual void OnSettingsMenuBackButtonClickHandler(object _, GameSettings currentSettings)
        {
            SettingsService.SetGameSettings(currentSettings);
            GameSettingsMenu.gameObject.SetActive(false);
        }

        #endregion
    }
}