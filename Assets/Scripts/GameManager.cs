using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class GameManager : MonoBehaviour, ISubscribing
    {
        #region Variables and constants

        private ShaftComponent Shaft { get; set; }
        private BallComponent Ball { get; set; }

        private BatComponent Bat01 { get; set; }
        private BatComponent Bat02 { get; set; }

        private GoalComponent Goal01 { get; set; }
        private GoalComponent Goal02 { get; set; }

        private IInputable Inputs { get; set; }

        private BatSettings BatSettings { get; set; }

        private Coroutine ballMovingCoroutine;

        private bool isGoalScored = false;

        private int hp = 10;

        private static float blockSize = 0.8f;
        private float blockRotateStep = 45.0f;
        private float distanceBetweenBlocks = blockSize + blockSize / 4;
        List<GameObject> blocks = new List<GameObject>(19);

        private int brokenBlockCounter = 0;

        #endregion

        #region Dependency Injection

        /// <summary>Псевдоконструктор для внедрения зависимостей Zenject.</summary>
        /// <param name="inputs">Источник ввода от пользователя.</param>
        [Inject]
        public void Construct(ShaftComponent shaft, BallComponent ball, List<BatComponent> bats, List<GoalComponent> goals, IInputable inputs, BatSettings batSettings)
        {
            Shaft = shaft;
            Ball = ball;

            Bat01 = bats.Where(ba => ba.name == "Bat01").FirstOrDefault();
            Bat02 = bats.Where(ba => ba.name == "Bat02").FirstOrDefault();

            Goal01 = goals.Where(go => go.name == "Goal01").FirstOrDefault();
            Goal02 = goals.Where(go => go.name == "Goal02").FirstOrDefault();

            Inputs = inputs;

            BatSettings = batSettings;

            Subscribe();

            Bat01.SetComponentAsParent(true, Ball.transform);

            Debug.Log("Начало новой игры\nВыбей все блоки и не потеряй все жизни, чтобы выиграть!");
            Debug.Log($"Текущее количество жизней: {hp}.");

            CreateBlocks();
        }

        #endregion

        #region MonoBehaviour methods

        private void OnDisable()
        {
            Unsubscribing();
        }
        #endregion

        #region Event handlers

        /// <summary>Обрабатывает ввод от какого-то игрока.</summary>
        /// <param name="_">Издатель события.</param>
        /// <param name="batDirection">Движение биты конкретного игрока.</param>
        private void SomePlayersInputHandler(object _, BatDirection batDirection)
        {
            if (!batDirection.IsInitialRoll)
            {
                switch (batDirection.Side)
                {
                    case SideOfConflict.First:
                        Bat01.Rigidbody.AddForce(batDirection.Movement * BatSettings.batSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
                        break;
                    case SideOfConflict.Second:
                        Bat02.Rigidbody.AddForce(batDirection.Movement * BatSettings.batSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
                        break;
                }
            }
            else
            {
                isGoalScored = false;

                if (Bat01.CheckIsObjectChild(Ball.transform))
                {
                    Bat01.SetComponentAsParent(false, Ball.transform);
                    Ball.Direction = Ball.transform.transform.forward;
                }
                else if (Bat02.CheckIsObjectChild(Ball.transform))
                {
                    Bat02.SetComponentAsParent(false, Ball.transform);
                    Ball.Direction = -Ball.transform.transform.forward;
                }

                ballMovingCoroutine = StartCoroutine(BallMovingCoroutine());
            }
        }

        /// <summary>Обрабатывает событие гола.</summary>
        /// <param name="_">Издатель события.</param>
        /// <param name="side">Сторона, которой забили гол.</param>
        private void BallInGoalEventHandler(object _, SideOfConflict side)
        {
            isGoalScored = true;
            hp -= 1;

            Debug.Log($"Текущее количество жизней: {hp}.");

            if (hp == 0)
            {
                UnityEditor.EditorApplication.isPaused = true;
                Debug.Log($"Game over. Вы проигирали :(");

            }
            else
            {
                Ball.SetBallSpeed(ChangeBallSpeed.Initial);

                switch (side)
                {
                    case SideOfConflict.First:
                        Bat01.SetComponentAsParent(true, Ball.transform);
                        break;
                    case SideOfConflict.Second:
                        Bat02.SetComponentAsParent(true, Ball.transform);
                        break;
                }
            }
        }

        private void BlockKnockEventHandler(object sender,  (GameObject go, Vector3 normal) item)
        {
            Ball.Direction = Vector3.Reflect(Ball.Direction.normalized, item.normal);

            if (item.go.name.Contains("Block"))
            {
                GameObject removed = blocks.Where(bl => bl.name == item.go.name).FirstOrDefault();

                if(removed != null)
                {
                    blocks.Remove(removed);
                    Destroy(removed);

                    brokenBlockCounter += 1;

                    if (blocks.Count() == 0)
                    {
                        UnityEditor.EditorApplication.isPaused = true;
                        Debug.Log($"Game over. Вы победили :)");
                    }
                    else
                    {
                        Ball.SetBallSpeed(ChangeBallSpeed.Up);
                        Debug.Log($"Счет: {brokenBlockCounter}, осталось блоков: {blocks.Count}");
                    }
                }
            }
        }

        #endregion

        #region Coroutines

        private IEnumerator BallMovingCoroutine()
        {
            while (!isGoalScored)
            {
                Ball.transform.Translate(Ball.Direction * Ball.currentBallSpeed * Time.deltaTime);
                yield return null;
            }
            ballMovingCoroutine = null;
            yield break;
        }

        #endregion

        #region Other methods

        public void Subscribe()
        {
            Inputs.BatDirectionEvent += SomePlayersInputHandler;
            Goal01.BallInGoalEvent += BallInGoalEventHandler;
            Goal02.BallInGoalEvent += BallInGoalEventHandler;
            Ball.BlockKnockEvent += BlockKnockEventHandler;
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.BatDirectionEvent -= SomePlayersInputHandler;
                Goal01.BallInGoalEvent -= BallInGoalEventHandler;
                Goal02.BallInGoalEvent -= BallInGoalEventHandler;
                Ball.BlockKnockEvent -= BlockKnockEventHandler;
            }
        }

        private void CreateBlocks()
        {
            Vector3 center = Shaft.transform.position;

            List<Vector3> blocksPositions = new List<Vector3>(19)
            {
                center,
                new Vector3(center.x + distanceBetweenBlocks, center.y, center.z),
                new Vector3(center.x - distanceBetweenBlocks, center.y, center.z),
                new Vector3(center.x, center.y + distanceBetweenBlocks, center.z),
                new Vector3(center.x, center.y - distanceBetweenBlocks, center.z),
                new Vector3(center.x, center.y, center.z + distanceBetweenBlocks),
                new Vector3(center.x, center.y, center.z - distanceBetweenBlocks),

                new Vector3(center.x + distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z),
                new Vector3(center.x + distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z),
                new Vector3(center.x - distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z),
                new Vector3(center.x - distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z),

                new Vector3(center.x + distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z + distanceBetweenBlocks),
                new Vector3(center.x + distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z - distanceBetweenBlocks),
                new Vector3(center.x + distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z + distanceBetweenBlocks),
                new Vector3(center.x + distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z - distanceBetweenBlocks),
                new Vector3(center.x - distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z + distanceBetweenBlocks),
                new Vector3(center.x - distanceBetweenBlocks, center.y + distanceBetweenBlocks, center.z - distanceBetweenBlocks),
                new Vector3(center.x - distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z + distanceBetweenBlocks),
                new Vector3(center.x - distanceBetweenBlocks, center.y - distanceBetweenBlocks, center.z - distanceBetweenBlocks),
            };

            for (int i = 0; i < blocksPositions.Count; i++)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.transform.localScale = new Vector3(blockSize, blockSize, blockSize);
                gameObject.transform.position = blocksPositions[i];
                gameObject.transform.Rotate(Random.Range(-blockRotateStep, blockRotateStep), Random.Range(-blockRotateStep, blockRotateStep), Random.Range(-blockRotateStep, blockRotateStep));
                gameObject.name = $"Block{i}";

                Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;

                blocks.Add(gameObject);
            }
        }

        #endregion
    }
}