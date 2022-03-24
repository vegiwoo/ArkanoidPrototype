using System;
using Arkanoid;
using UnityEngine;
using UnityEngine.UI;

public class PausedMenuController : MonoBehaviour
{
    #region Variables and constants

    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    public event EventHandler<PausedMenuCommand> PauseMenuEvent;

    #endregion

    #region MonoBehaviour Methods

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    #endregion

    #region Methods
    private void OnRestartButtonClick()
    {
        PauseMenuEvent?.Invoke(this, PausedMenuCommand.Restart);
    }

    private void OnSettingsButtonClick()
    {
        PauseMenuEvent?.Invoke(this, PausedMenuCommand.Settings);
    }

    private void OnResumeButtonClick()
    {
        PauseMenuEvent?.Invoke(this, PausedMenuCommand.Resume);
    }

    private void OnExitButtonClick()
    {
        PauseMenuEvent?.Invoke(this, PausedMenuCommand.Exit);
    }

    #endregion
}