using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class GameSettingsContoller : MonoBehaviour
    {
        [SerializeField] private Toggle soundEnableToggle;
        [SerializeField] private Slider soundVolumeSlider;
        [SerializeField] private GameObject difficultyToggles;
        private ToggleGroup difficultyToggleGroup;

        [SerializeField] private Button backButton;

        public GameSettings currentGameSettings;

        public event EventHandler<GameSettings> gameSettingsEvent;
        public event EventHandler<GameSettings> backEvent;

        private void Awake()
        {
            currentGameSettings = new GameSettings(DifficultyGameMode.Easy, false, 10);
            difficultyToggleGroup = difficultyToggles.GetComponent<ToggleGroup>();
        }

        private void OnEnable()
        {
            soundEnableToggle.onValueChanged.AddListener(OnSoundEnableToggleHandler);
            backButton.onClick.AddListener(OnBackButtonClickHandler);
        }

        private void OnDisable()
        {
            soundEnableToggle.onValueChanged.RemoveListener(OnSoundEnableToggleHandler);
            backButton.onClick.RemoveListener(OnBackButtonClickHandler);
        }

        public void UpdateSettings(GameSettings settings)
        {
            currentGameSettings = settings;
        }

        private void Start()
        {
            OnSoundEnableToggleHandler(false);

            soundEnableToggle.isOn = currentGameSettings.IsMuteSound;
           

            soundVolumeSlider.enabled = true;
            soundVolumeSlider.value = currentGameSettings.CurrentSoundVolumeLevel;
            soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderHandler);

            print("GameSettingsContoller Start");
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

            gameSettingsEvent?.Invoke(this, currentGameSettings);
        }

        // TODO: Обработка по режиму сложности.

        private void OnBackButtonClickHandler()
        {
            backEvent?.Invoke(this, currentGameSettings);
        }
    }
}