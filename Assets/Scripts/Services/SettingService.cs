using System;
using UnityEngine;

namespace Arkanoid
{
    /// <summary>Сервис для работы с настройками игры.</summary>
    public class SettingService : ISettingServiceble
    {
        #region Variables and constants

        private const int BASE_SOUND_LEVEL = 80;

        string difficultyKey = KeyForSavingParams.GameMode.ToString();
        string isMuteSoundKey = KeyForSavingParams.IsMuteSound.ToString();
        string currentSoundVolumeLevelKey = KeyForSavingParams.CurrentSoundVolumeLevel.ToString();

        private GameSettings currentGameSettings;

        #endregion

        #region Initialization and deinitialization

        private SettingService()
        {
            // PlayerPrefs.DeleteAll();
            currentGameSettings = GetGameSettingsFromStorage();
        }

        #endregion

        #region Methods

        public GameSettings GetGameSettings()
        {
            return currentGameSettings;
        }

        public void SetGameSettings(GameSettings settings)
        {
            currentGameSettings = settings;
            SaveGameSettingsInStorage();
        }

        /// <summary>Сохраняет параметры игры в хранилище данных.</summary>
        private void SaveGameSettingsInStorage()
        {
            int difficultyValue = (int)currentGameSettings.GameMode;

            int isMuteSoundValue = Convert.ToInt32(currentGameSettings.IsMuteSound);
            int currentSoundVolumeLevelValue = currentGameSettings.CurrentSoundVolumeLevel;

            PlayerPrefs.SetInt(difficultyKey, difficultyValue);
            PlayerPrefs.SetInt(isMuteSoundKey, isMuteSoundValue);
            PlayerPrefs.SetInt(currentSoundVolumeLevelKey, currentSoundVolumeLevelValue);

            SaveStorage();
        }

        /// <summary>Получает параметры игры из хранилища данных создает новые.</summary>
        /// <returns>Сфорированный экземпляр параметров.</returns>
        private GameSettings GetGameSettingsFromStorage()
        {
            GameSettings gameSettings = new GameSettings(DifficultyGameMode.Easy, false, BASE_SOUND_LEVEL);

            // Get DifficultyGameMode
            if (PlayerPrefs.HasKey(difficultyKey))
            {
                int gameModeValue = PlayerPrefs.GetInt(difficultyKey);
                gameSettings.GameMode = (DifficultyGameMode)gameModeValue;
            }

            // Get IsMuteSound
            if (PlayerPrefs.HasKey(isMuteSoundKey))
            {
                int IsMuteSoundValue = PlayerPrefs.GetInt(isMuteSoundKey);
                gameSettings.IsMuteSound = Convert.ToBoolean(IsMuteSoundValue);
            }

            // Get CurrentSoundVolumeLevel
            if (PlayerPrefs.HasKey(currentSoundVolumeLevelKey))
            {
                gameSettings.CurrentSoundVolumeLevel = PlayerPrefs.GetInt(currentSoundVolumeLevelKey);
            }

            return gameSettings;
        }

        public void SaveStorage()
        {
            PlayerPrefs.Save();
        }

        #endregion
    }
}