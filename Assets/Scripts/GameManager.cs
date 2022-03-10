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

        private IInputable Inputs { get; set; }

        private BatSettings BatSettings { get; set; }

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
            Subscribe();

            BatSettings = batSettings;
        }

        #endregion

        #region MonoBehaviour methods

        private void FixedUpdate()
        {
            if (Bat01.Movement != Vector2.zero)
                Bat01.Rigidbody.AddForce(Bat01.Movement * BatSettings.batSpeed, ForceMode.Acceleration);
            if (Bat02.Movement != Vector2.zero)
                Bat02.Rigidbody.AddForce(Bat02.Movement * BatSettings.batSpeed, ForceMode.Acceleration);
        } 

        private void OnDisable()
        {
            Unsubscribing();
        }
        #endregion

        #region Other methods

        public void Subscribe()
        {
            Inputs.BitDirectionEvent += SomePlayersInputHandler;
        }

        public void Unsubscribing()
        {
            if (Inputs != null)
            {
                Inputs.BitDirectionEvent -= SomePlayersInputHandler;
            }
        }

        #endregion

        /// <summary>Обрабатывает ввод от какого-то игрока.</summary>
        /// <param name="_">Издатель события;</param>
        /// <param name="batDirection">Движение биты конкретного игрока.</param>
        private void SomePlayersInputHandler(object _, BatDirection batDirection)
        {
            switch (batDirection.Side)
            {
                case SideOfConflict.First:
                    Bat01.Movement = batDirection.Movement;
                    break;
                case SideOfConflict.Second:
                    Bat02.Movement = batDirection.Movement;
                    break;
            }
        }
    }
}