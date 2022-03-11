using UnityEngine;

namespace Arkanoid
{
    public class BallComponent : MonoBehaviour
    {
        public const float MIN_BALL_SPEED = 2.5f;
        public const float MAX_BALL_SPEED = 8.0f;

        public float currentBallSpeed = default;

        private void Start()
        {
            currentBallSpeed = MIN_BALL_SPEED;
        }

    }
}