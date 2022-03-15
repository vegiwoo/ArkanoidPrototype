using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/*
 * Мяч не должен перемещаться и отражаться по физике (векторная математика)
    Ворота за игроками
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

        private IInputable Inputs { get; set; }

        private BatSettings BatSettings { get; set; }

        private Coroutine ballMovingCoroutine;

        private ContactWithObject? BallContact;

        #endregion

        #region Dependency Injection

        /// <summary>Псевдоконструктор для внедрения зависимостей Zenject.</summary>
        /// <param name="inputs">Источник ввода от пользователя.</param>
        [Inject]
        public void Construct(ShaftComponent shaft, BallComponent ball, List<BatComponent> bats, IInputable inputs, BatSettings batSettings)
        {
            Shaft = shaft;
            Ball = ball;

            Bat01 = bats.Where(ba => ba.name == "Bat01").FirstOrDefault();
            Bat02 = bats.Where(ba => ba.name == "Bat02").FirstOrDefault();

            Inputs = inputs;

            BatSettings = batSettings;

            Subscribe();

            Bat01.SetComponentAsParent(true, Ball.transform);
        }

        private void BallContactEventHandler(object sender, List<ContactWithObject> contactList)
        {
            foreach (ContactWithObject contact in contactList)
            {
                if (contact.ContactTargetName == Ball.name)
                {
                    print("Boom!");
                    //ballMovingCoroutine = StartCoroutine(BallMovingCoroutine());
                }
            }
        }

        #endregion

        private void RemovingBallFromBat()
        {
            if (Bat01.CheckIsObjectChild(Ball.gameObject.transform))
            {
                Bat01.SetComponentAsParent(false, Ball.transform);
            }

            if (Bat02.CheckIsObjectChild(Ball.gameObject.transform))
            {
                Bat02.SetComponentAsParent(false, Ball.transform);
            }
        }

        #region MonoBehaviour methods

        private void OnDisable()
        {
            Unsubscribing();
        }
        #endregion

        #region Other methods

        public void Subscribe()
        {
            Inputs.BatDirectionEvent += SomePlayersInputHandler;
            Ball.BallContactEvent += BallContactEventHandler;
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.BatDirectionEvent -= SomePlayersInputHandler;
            }

            if (Ball != null)
            {
                Ball.BallContactEvent -= BallContactEventHandler;
            }
        }

        #endregion

        /// <summary>Обрабатывает ввод от какого-то игрока.</summary>
        /// <param name="_">Издатель события;</param>
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
                Ball.direction = Ball.transform.forward;



                ballMovingCoroutine = StartCoroutine(BallMovingCoroutine());
            }
        }


        private IEnumerator BallMovingCoroutine()
        {
            while (true)
            {
                Ball.transform.Translate(Ball.direction * Ball.currentBallSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}