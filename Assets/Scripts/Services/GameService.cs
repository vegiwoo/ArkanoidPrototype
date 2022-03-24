using UnityEngine;

namespace Arkanoid
{
    public class GameService : IGameServiceble
    {
        private ISettingServiceble Setting;

        private bool isGamePaused = false;

        public GameService(ISettingServiceble settings)
        {
            Setting = settings;
        }

        public bool TogglePaused()
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0 : 1;
            return isGamePaused;
        }

        public void ExitGame()
        {
            if (Application.isPlaying)
            {
                Setting.SaveStorage();

                if (Application.isEditor)
                {
                    Debug.Log("Выход из игры на движке");
                    UnityEditor.EditorApplication.ExitPlaymode();
                }
                else
                {
                    Debug.Log("Выход из игры на билде");
                    Application.Quit();
                }
            }
        }
    }
}