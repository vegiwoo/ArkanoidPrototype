using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Arkanoid
{
    /// <summary>Имена всех сцен в проекте.</summary>
    public enum Scene
    {
        MainScene, GameScene
    }

    public interface ISceneble
    {
        /// <summary>Загружает сцену по имени.</summary>
        /// <param name="scene">Имя сцены.</param>
        void LoadScene(Scene scene);
    }

    public class SceneController : ISceneble
    {
        public SceneController()
        {
            Debug.Log("SceneController binding");
        }

        public void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}




