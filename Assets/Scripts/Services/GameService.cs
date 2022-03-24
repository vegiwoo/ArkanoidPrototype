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

        public void TogglePaused()
        {
            isGamePaused = !isGamePaused;

            switch (isGamePaused)
            {
                case true:
                    Time.timeScale = 0;
                    break;
                case false:
                    Time.timeScale = 1;
                    break;
            }
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