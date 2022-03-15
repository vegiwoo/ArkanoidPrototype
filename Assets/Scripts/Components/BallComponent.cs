using UnityEngine;

namespace Arkanoid
{
    public class BallComponent : MonoBehaviour
    {
        public const float MIN_BALL_SPEED = 3.0f;
        public const float MAX_BALL_SPEED = 8.0f;

        public float currentBallSpeed = default;

        public Vector3 Direction;

        private void Start()
        {
            currentBallSpeed = MIN_BALL_SPEED;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Direction = Vector3.Reflect(Direction.normalized, collision.contacts[0].normal);
        }
    }
}