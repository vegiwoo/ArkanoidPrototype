using System;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    /// <summary>Инсталлер необходимых игровых объектов.</summary>
    public class ObjectsInstaller : MonoInstaller
    {
        [SerializeField] private ShaftComponent shaft;
        [SerializeField] private Transform shaftTransform;
        [SerializeField] private BallComponent ball;
        [SerializeField] private Transform ballTransform;
        [SerializeField] private BatComponent bat01;
        [SerializeField] private Transform bat01Transform;
        [SerializeField] private BatComponent bat02;
        [SerializeField] private Transform bat02Transform;

        public override void InstallBindings()
        {
            // Shaft
            ShaftComponent shaftComponent = Container.InstantiatePrefabForComponent<ShaftComponent>(shaft.gameObject, shaftTransform.position, shaftTransform.rotation, null);
            shaftComponent.name = "Shaft";
            Container.Bind<ShaftComponent>().FromInstance(shaftComponent).AsSingle().NonLazy();

            // Ball
            BallComponent ballComponent = Container.InstantiatePrefabForComponent<BallComponent>(ball.gameObject, ballTransform.position, ballTransform.rotation, null);
            ballComponent.name = "Ball";
            Container.Bind<BallComponent>().FromInstance(ballComponent).AsSingle().NonLazy();

            // Bat01
            BatComponent bat01Component = Container.InstantiatePrefabForComponent<BatComponent>(bat01.gameObject, bat01Transform.transform.position, bat01Transform.rotation, null);
            bat01Component.name = "Bat01";
            Container.Bind<BatComponent>().FromInstance(bat01Component).NonLazy();

            // Bat02
            BatComponent bat02Component = Container.InstantiatePrefabForComponent<BatComponent>(bat02.gameObject, bat02Transform.position, bat02Transform.rotation, null);
            bat02Component.name = "Bat02";
            Container.Bind<BatComponent>().FromInstance(bat02Component).NonLazy();
        }
    }
}