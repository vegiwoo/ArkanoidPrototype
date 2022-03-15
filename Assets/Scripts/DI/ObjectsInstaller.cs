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
        [SerializeField] private BatComponent bat01;
        [SerializeField] private Transform bat01Transform;
        [SerializeField] private BatComponent bat02;
        [SerializeField] private Transform bat02Transform;

        [SerializeField] private GoalComponent goal01;
        [SerializeField] private Transform goal01Transform;
        [SerializeField] private GoalComponent goal02;
        [SerializeField] private Transform goal02Transform;

        public override void InstallBindings()
        {
            // Shaft
            ShaftComponent shaftComponent = Container.InstantiatePrefabForComponent<ShaftComponent>(shaft.gameObject, shaftTransform.position, shaftTransform.rotation, null);
            shaftComponent.name = "Shaft";
            Container.Bind<ShaftComponent>().FromInstance(shaftComponent).AsSingle().NonLazy();

            // Ball
            BallComponent ballComponent = Container.InstantiatePrefabForComponent<BallComponent>(ball.gameObject, ball.gameObject.transform.position, ball.gameObject.transform.rotation, null);
            ballComponent.name = "Ball";
            Container.Bind<BallComponent>().FromInstance(ballComponent).AsSingle().NonLazy();

            // Bat01
            BatComponent bat01Component = Container.InstantiatePrefabForComponent<BatComponent>(bat01.gameObject, bat01Transform.transform.position, bat01Transform.rotation, null);
            bat01Component.name = "Bat01";
            bat01Component.Side = SideOfConflict.First;
            Container.Bind<BatComponent>().FromInstance(bat01Component).NonLazy();

            // Bat02
            BatComponent bat02Component = Container.InstantiatePrefabForComponent<BatComponent>(bat02.gameObject, bat02Transform.position, bat02Transform.rotation, null);
            bat02Component.name = "Bat02";
            bat01Component.Side = SideOfConflict.Second;
            Container.Bind<BatComponent>().FromInstance(bat02Component).NonLazy();

            // Goal01
            GoalComponent goal01Component = Container.InstantiatePrefabForComponent<GoalComponent>(goal01.gameObject, goal01Transform.transform.position, goal01Transform.rotation, null);
            goal01Component.name = "Goal01";
            goal01Component.Side = SideOfConflict.First;
            Container.Bind<GoalComponent>().FromInstance(goal01Component).NonLazy();

            // Goal02
            GoalComponent goal02Component = Container.InstantiatePrefabForComponent<GoalComponent>(goal02.gameObject, goal02Transform.transform.position, goal02Transform.rotation, null);
            goal02Component.name = "Goal02";
            goal02Component.Side = SideOfConflict.Second;
            Container.Bind<GoalComponent>().FromInstance(goal02Component).NonLazy();
        }
    }
}