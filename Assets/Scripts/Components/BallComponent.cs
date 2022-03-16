using System;
using UnityEngine;

namespace Arkanoid
{
    public class BallComponent : MonoBehaviour
    {
        private const float MIN_BALL_SPEED = 3.0f;
        private const float MAX_BALL_SPEED = 5.0f;
        private const float STEP_BALL_SPEED = 0.35f;

        /// <summary>Текущая скорость мяча*</summary>
        public float currentBallSpeed = default;

        public Vector3 Direction;

        public event EventHandler<(GameObject, Vector3)> BlockKnockEvent;

        private void Start()
        {
            SetBallSpeed(ChangeBallSpeed.Initial);
        }

        private void OnCollisionEnter(Collision collision)
        {
            BlockKnockEvent?.Invoke(this, (collision.gameObject, collision.contacts[0].normal));
        }

        public void SetBallSpeed(ChangeBallSpeed change)
        {
            switch (change)
            {
                case ChangeBallSpeed.Initial:
                    currentBallSpeed = MIN_BALL_SPEED;
                    break;
                case ChangeBallSpeed.Up:
                    if (currentBallSpeed < MAX_BALL_SPEED && currentBallSpeed + STEP_BALL_SPEED < MAX_BALL_SPEED)
                        currentBallSpeed += STEP_BALL_SPEED;
                    break;
            }
        }
    }
}

