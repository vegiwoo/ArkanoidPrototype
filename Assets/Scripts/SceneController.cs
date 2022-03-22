using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid
{
    public class SceneController : ISceneble
    {
        public void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}