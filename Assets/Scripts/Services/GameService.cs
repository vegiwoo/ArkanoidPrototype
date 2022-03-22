using UnityEngine;

namespace Arkanoid
{
    public class GameService : IGameServiceble
    {
        public void ExitGame()
        {
            if (Application.isPlaying)
            {
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