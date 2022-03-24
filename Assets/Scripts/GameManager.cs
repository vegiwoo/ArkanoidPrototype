using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Arkanoid
{
    public class GameManager : BaseManager, ISubscribing
    {
        #region Variables and constants

        // Службы
        private IInputServiceble Inputs { get; set; }

        // Меню
        private PausedMenuController PausedMenu { get; set; }

        private ShaftComponent Shaft { get; set; }
        private BallComponent Ball { get; set; }

        private BatComponent Bat01 { get; set; }
        private BatComponent Bat02 { get; set; }

        private GoalComponent Goal01 { get; set; }
        private GoalComponent Goal02 { get; set; }

        private BatSettings BatSettings { get; set; }

        private Coroutine ballMovingCoroutine;

        private bool isGoalScored = false;

        private int hp = 10;

        private static float blockSize = 0.8f;
        private float blockRotateStep = 45.0f;
        private float distanceBetweenBlocks = blockSize + blockSize / 4;
        List<GameObject> blocks = new List<GameObject>(19);

        private int brokenBlockCounter = 0;

        private List<Text> scoreLabels = new List<Text>(0);

        #endregion

        #region Dependency Injection

        /// <summary>Псевдоконструктор для внедрения зависимостей (службы).</summary>
        [Inject]
        public void ConstructServices(IGameServiceble game, IInputServiceble inputs, ISettingServiceble setting, ISceneble sceneService)
        {
            GameService = game;
            Inputs = inputs;
            SettingsService = setting;
            SceneService = sceneService;
        }

        /// <summary>Псевдоконструктор для внедрения зависимостей (меню).</summary>
        [Inject]
        public void ConstructMenues(PausedMenuController pausedMenu, GameSettingsContoller gameSettings)
        {
            PausedMenu = pausedMenu;
            GameSettingsMenu = gameSettings;
        }

        [Inject]
        public void Construct(ShaftComponent shaft, BallComponent ball, List<BatComponent> bats, List<GoalComponent> goals, BatSettings batSettings)
        {
            Shaft = shaft;
            Ball = ball;

            Bat01 = bats.Where(ba => ba.name == "Bat01").FirstOrDefault();
            Bat02 = bats.Where(ba => ba.name == "Bat02").FirstOrDefault();

            Goal01 = goals.Where(go => go.name == "Goal01").FirstOrDefault();
            Goal02 = goals.Where(go => go.name == "Goal02").FirstOrDefault();

            BatSettings = batSettings;

            Subscribe();

            Bat01.SetComponentAsParent(true, Ball.transform);

            Debug.Log("Начало новой игры\nВыбей все блоки и не потеряй все жизни, чтобы выиграть!");

            CreateBlocks(Shaft.transform.position);

            AddMenuToCanvasBat01();

            scoreLabels.Add(Bat01.scoreLabel);
            scoreLabels.Add(Bat02.scoreLabel);

            foreach (var label in scoreLabels)
            {
                label.text = hp.ToString();
            }
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
                if (ballMovingCoroutine != null) return;

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

            foreach (var label in scoreLabels)
            {
                label.text = hp.ToString();
            }

            Debug.Log($"Текущее количество жизней: {hp}.");

            if (hp == 0)
            {
                UnityEditor.EditorApplication.isPaused = true;
                Debug.Log($"Game over. Вы проиграли :(");
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
            Inputs.PauseEvent += PauseEventHandler;

            Inputs.BatDirectionEvent += SomePlayersInputHandler;
            Goal01.BallInGoalEvent += BallInGoalEventHandler;
            Goal02.BallInGoalEvent += BallInGoalEventHandler;
            Ball.BlockKnockEvent += BlockKnockEventHandler;

            PausedMenu.PauseMenuEvent += OnPauseMenuEvent;
            GameSettingsMenu.backEvent += OnSettingsMenuBackButtonClickHandler;
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.PauseEvent -= PauseEventHandler;

                Inputs.BatDirectionEvent -= SomePlayersInputHandler;
                Goal01.BallInGoalEvent -= BallInGoalEventHandler;
                Goal02.BallInGoalEvent -= BallInGoalEventHandler;
                Ball.BlockKnockEvent -= BlockKnockEventHandler;

                PausedMenu.PauseMenuEvent -= OnPauseMenuEvent;
                GameSettingsMenu.backEvent -= OnSettingsMenuBackButtonClickHandler;
            }
        }

        /// <summary>Создает игровые блоки по заранее опереленным координатам.</summary>
        private void CreateBlocks(Vector3 center)
        {
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

        public void AddMenuToCanvasBat01()
        {
            PausedMenu.transform.SetParent(Bat01.canvas.transform, false);
            GameSettingsMenu.transform.SetParent(Bat01.canvas.transform, false);

            SetMenuSetting(PausedMenu.gameObject, false);
            SetMenuSetting(GameSettingsMenu.gameObject, false);
        }

        private void PauseEventHandler(object _, bool toggle)
        {
            bool currentPaused = GameService.TogglePaused();

            if (currentPaused)
            {
                PausedMenu.gameObject.SetActive(true);
            }
            else
            {
                PausedMenu.gameObject.SetActive(false);
            }
        }

        private void OnPauseMenuEvent(object _, PausedMenuCommand command)
        {
            switch (command)
            {
                case PausedMenuCommand.Restart:
                    Debug.Log("Перезапуск игры");
                    PauseEventHandler(this, true);
                    SceneService.LoadScene(Scene.GameScene);
                    break;
                case PausedMenuCommand.Settings:
                    Debug.Log("Переход в меню настроек");
                    PausedMenu.gameObject.SetActive(false);
                    GameSettingsMenu.UpdateSettings(SettingsService.GetGameSettings());
                    GameSettingsMenu.gameObject.SetActive(true);
                    break;
                case PausedMenuCommand.Resume:
                    Debug.Log("Продолжить игру");
                    PauseEventHandler(this, true);
                    break;
                case PausedMenuCommand.Exit:
                    Debug.Log("Выйти из игры");
                    GameService.ExitGame();
                    break;
            }
        }

        protected override void OnSettingsMenuBackButtonClickHandler(object sender, GameSettings currentSettings)
        {
            base.OnSettingsMenuBackButtonClickHandler(sender, currentSettings);
            PausedMenu.gameObject.SetActive(true);
        }

        #endregion
    }
}