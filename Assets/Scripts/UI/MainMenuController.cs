using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        public event EventHandler<MainMenuCommand> mainMenuEvent;

        private void OnEnable()
        {
            newGameButton.onClick.AddListener(OnClickNewGameButtonHandler);
            settingsButton.onClick.AddListener(OnClickSettingsButtonHandler);
            exitButton.onClick.AddListener(OnClickExitButtonHandler);
        }

        private void OnDisable()
        {
            newGameButton.onClick.RemoveListener(OnClickNewGameButtonHandler);
            settingsButton.onClick.RemoveListener(OnClickSettingsButtonHandler);
            exitButton.onClick.RemoveListener(OnClickExitButtonHandler);
        }

        private void OnClickNewGameButtonHandler()
        {
            mainMenuEvent?.Invoke(this, MainMenuCommand.NewGame);
        }

        private void OnClickSettingsButtonHandler()
        {
            mainMenuEvent?.Invoke(this, MainMenuCommand.Settings);
        }

        private void OnClickExitButtonHandler()
        {
            mainMenuEvent?.Invoke(this, MainMenuCommand.Exit);
        }
    }
}