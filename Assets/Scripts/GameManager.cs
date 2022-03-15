using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/*
 *
    Мяч возвращается пропустившему игроку, у понижается общий счет жизней (счет общий)
    Мяч отражается от платформ, стен, блоков (блок удалятся)
    После удара по блоку мяч ускоряется (есть мнимальная и максималная скорость)
    - при упускании шарика скорость возвращается к минимальной
    Блоки в центре тоннеля, генерация, смещение (random rotation)
    Конец игры - оба игрока пропустят по N шариков или выбиты все блоки
    Все настройки перенести в редактор
    Комментарии


    Найти метод, реализующий отражение без физики
 */ 

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
                Debug.Log($"Game over.");

            }
            else
            {
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
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.BatDirectionEvent -= SomePlayersInputHandler;
                Goal01.BallInGoalEvent -= BallInGoalEventHandler;
                Goal02.BallInGoalEvent -= BallInGoalEventHandler;
            }
        }

        #endregion
    }
}