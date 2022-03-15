using System;
using UnityEngine;

namespace Arkanoid
{
    public class GoalComponent : MonoBehaviour, ISideble
    {
        public SideOfConflict Side { get; set; }

        public event EventHandler<SideOfConflict> BallInGoalEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("Ball"))
                BallInGoalEvent?.Invoke(this, Side);
        }
    }
}