using UnityEngine;
using Zenject;

namespace Arkanoid 
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameManager gameManager;
        public BatSettings batSettings;

        public override void InstallBindings()
        {
            Container.Bind<IInputable>().To<InputController>().AsSingle().NonLazy();

            // BatSettings
            batSettings.batSpeed = 50.0f;
            Container.Bind<BatSettings>().FromInstance(batSettings).AsSingle().NonLazy();

            // GameManager
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();
        }
    }
}