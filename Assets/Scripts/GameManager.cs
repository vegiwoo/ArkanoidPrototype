using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/*
 * 
 * Мяч - есть все компоненты физического тела, коллайдер, но заморожено перемещение и вращение
 * В начале игры мяч у первого игрока, запуск по пробелу 
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

            Ball.transform.position = Bat01.cameraPosition;

            print($"Dependency injection for GameManager was successful.");
        }

        #endregion

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
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.BatDirectionEvent -= SomePlayersInputHandler;
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
                switch (batDirection.Side)
                {
                    case SideOfConflict.First:
                        Ball.transform.position = Bat01.cameraPosition;
                        break;
                    case SideOfConflict.Second:
                        Ball.transform.position = Bat02.cameraPosition;
                        break;
                }

                ballMovingCoroutine = StartCoroutine(BallMovingCoroutine());
            }
        }



        private IEnumerator BallMovingCoroutine()
        {
            // Придеть укорение мячику вперед 





            // Получать Reflected Object через RayCast



            //float maxDistance = 10;



            //Vector3 direction = 3 * Ball.transform.forward;
            //Vector3 multiplicationScalar = 5 * a; 

            while (true)
            {
                var direction = Ball.transform.forward * 2;

                Ball.transform.Translate(Ball.transform.forward * Time.deltaTime);
                Debug.DrawLine(Ball.transform.position, direction, Color.red, 2);

                //Ball.transform.Translate(Ball.transform.forward * Time.deltaTime);
                //direction = 5 * Ball.transform.position;


                //Vector3 forward = Ball.transform.TransformDirection(Vector3.forward);

                //if (Physics.Raycast(Ball.transform.position, forward, maxDistance))
                //{
                //    // Впереди перед объектом что-то есть
                //}




                //Ball.transform.Translate(Ball.transform.forward * Ball.currentBallSpeed * Time.deltaTime);
                yield return null;
            }


           


            //Ball.transform.position = Vector3.Lerp(Ball.transform.position, ballDirection, 1);

           
        }
    }
}