using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BallComponent : MonoBehaviour
    {
        public const float MIN_BALL_SPEED = 3.0f;
        public const float MAX_BALL_SPEED = 8.0f;

        public float currentBallSpeed = default;

        public Vector3 direction;

        /// <summary>Событие столкновения шара с другими объектами.</summary>
        public event EventHandler<List<ContactWithObject>> BallContactEvent;

        private void Start()
        {
            currentBallSpeed = MIN_BALL_SPEED;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject.name.Contains("Bat"))
            //{


                direction = -direction;

           // }
        }
    }
}