using System;
using UnityEngine;

namespace Arkanoid
{
    [Serializable]
    public struct BatSettings
    {
        /// <summary>Скорость биты.</summary>
        [SerializeField] public float batSpeed;
    }

    /// <summary>Движение биты игрока.</summary>
    public struct BatDirection
    {
        public SideOfConflict Side { get; private set; }
        /// <summary> Направление движения биты игрока.</summary>
        public Vector2 Movement { get; private set; }

        public bool IsInitialRoll { get; private set; }

        public BatDirection(SideOfConflict side, Vector2 movement)
        {
            Side = side;
            Movement = movement;
            IsInitialRoll = false;
        }

        public BatDirection(SideOfConflict side, bool isInitialRoll)
        {
            Side = side;
            Movement = Vector2.zero;
            IsInitialRoll = isInitialRoll;
        }
    }

}