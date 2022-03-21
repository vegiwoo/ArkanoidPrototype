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

        private DifficultyGameMode gameMode = DifficultyGameMode.Easy;
        private bool isMuteSound = false;

        private const int SOUND_VOLUME_LEVEL_DEFAULT = 50; 
        private int soundVolumeLevel = SOUND_VOLUME_LEVEL_DEFAULT;

        private void Awake()
        {
            difficultyToggleGroup = difficultyToggles.GetComponent<ToggleGroup>();
        }

        private void Start()
        {
            soundEnableToggle.isOn = isMuteSound;
            soundEnableToggle.onValueChanged.AddListener(OnSoundEnableToggleHandler);

            soundVolumeSlider.enabled = true;
            soundVolumeSlider.value = soundVolumeLevel;
            soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderHandler);

            gameMode = DifficultyGameMode.Easy;
        }

        private void OnSoundEnableToggleHandler(bool value)
        {
            isMuteSound = value;

            if (isMuteSound)
            {
                OnSoundVolumeSliderHandler(0);
                soundVolumeSlider.enabled = false;
            }
            else
            {
                OnSoundVolumeSliderHandler(SOUND_VOLUME_LEVEL_DEFAULT);
                soundVolumeSlider.enabled = true;
            }
        }

        private void OnSoundVolumeSliderHandler(float value)
        {
            soundVolumeLevel = (int)value;

            if (soundVolumeSlider.value != soundVolumeLevel)
                soundVolumeSlider.value = soundVolumeLevel;
        }
    }
}