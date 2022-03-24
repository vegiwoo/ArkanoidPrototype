using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class GameSettingsContoller : MonoBehaviour
    {
        #region Variables and constants
        
        [SerializeField] private Toggle soundEnableToggle;
        [SerializeField] private Slider soundVolumeSlider;
        [SerializeField] private ToggleGroup difficultyToggles;
        private List<Toggle> difficultyTogglesList = new List<Toggle>(3);
        [SerializeField] private Button backButton;

        public GameSettings currentGameSettings;
        public event EventHandler<GameSettings> backEvent;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            currentGameSettings = new GameSettings(DifficultyGameMode.Easy, false, 10);
            difficultyTogglesList = difficultyToggles.GetComponentsInChildren<Toggle>().ToList();
        }

        private void OnEnable()
        {
            soundEnableToggle.onValueChanged.AddListener(OnSoundEnableToggleHandler);
            soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderHandler);

            foreach (var toggle in difficultyTogglesList)
            {
                toggle.onValueChanged.AddListener(OnDifficultyTogglesHandler);
            }

            backButton.onClick.AddListener(OnBackButtonClickHandler);
        }

        private void OnDisable()
        {
            soundEnableToggle.onValueChanged.RemoveListener(OnSoundEnableToggleHandler);
            soundVolumeSlider.onValueChanged.RemoveListener(OnSoundVolumeSliderHandler);

            foreach (var toggle in difficultyTogglesList)
            {
                toggle.onValueChanged.RemoveListener(OnDifficultyTogglesHandler);
            }

            backButton.onClick.RemoveListener(OnBackButtonClickHandler);
        }

        #endregion

        #region Methods

        public void UpdateSettings(GameSettings settings)
        {
            currentGameSettings = settings;

            soundEnableToggle.isOn = soundVolumeSlider.enabled  = currentGameSettings.IsMuteSound;
            soundVolumeSlider.value = currentGameSettings.CurrentSoundVolumeLevel;

            int gameModeIndex = (int)currentGameSettings.GameMode;
            List<Toggle> toggles = difficultyToggles.GetComponentsInChildren<Toggle>().ToList();
            toggles[gameModeIndex].isOn = true;
        }

        private void OnSoundEnableToggleHandler(bool value)
        {
            currentGameSettings.IsMuteSound = value;

            if (currentGameSettings.IsMuteSound)
            {
                OnSoundVolumeSliderHandler(0);
                soundVolumeSlider.enabled = false;
            }
            else
            {
                OnSoundVolumeSliderHandler(currentGameSettings.CurrentSoundVolumeLevel);
                soundVolumeSlider.enabled = true;
            }
        }

        private void OnSoundVolumeSliderHandler(float value)
        {
            currentGameSettings.CurrentSoundVolumeLevel = (int)value;

            if (soundVolumeSlider.value != currentGameSettings.CurrentSoundVolumeLevel)
                soundVolumeSlider.value = currentGameSettings.CurrentSoundVolumeLevel;
        }

        private void OnDifficultyTogglesHandler(bool _)
        {
            Toggle activeToggle = difficultyToggles.ActiveToggles().FirstOrDefault();
            int activeToggleIndex = difficultyTogglesList.IndexOf(activeToggle);

            DifficultyGameMode selectGameMode = (DifficultyGameMode)activeToggleIndex;
            currentGameSettings.GameMode = selectGameMode;
        }

        private void OnBackButtonClickHandler()
        {
            backEvent?.Invoke(this, currentGameSettings);
        }
        #endregion
    }
}