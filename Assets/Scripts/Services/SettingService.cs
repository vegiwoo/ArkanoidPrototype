using UnityEngine;

namespace Arkanoid
{
    public class SettingService : ISettingServicable
    {
        private const int BASE_SOUND_LEVEL = 80;

        private GameSettings currentGameSettings;

        private SettingService()
        {
            currentGameSettings = new GameSettings(DifficultyGameMode.Easy, false, BASE_SOUND_LEVEL);
        }

        public GameSettings GetGameSettings()
        {
            return currentGameSettings;
        }

        public void SetGameSettings(GameSettings settings)
        {
            currentGameSettings = settings;
        }
    }
}