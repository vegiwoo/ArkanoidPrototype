using System;
using UnityEngine;

namespace Arkanoid
{
    /// <summary>Настройки игры.</summary>
    public struct GameSettings
    {
        public DifficultyGameMode GameMode { get; set; }
        /// <summary>Включен или выключен звук в игре.</summary>
        public bool IsMuteSound { get; set; }
        /// <summary>Текущая громкость звука. </summary>
        public int CurrentSoundVolumeLevel { get; set; }

        public GameSettings(DifficultyGameMode gameMode, bool isMuteSound, int volume)
        {
            GameMode = gameMode;
            IsMuteSound = isMuteSound;
            CurrentSoundVolumeLevel = volume;
        }
    }

    [Serializable]
    public struct BatSettings
    {
        /// <summary>Скорость биты.</summary>
        [SerializeField] public float batSpeed;
    }

    /// <summary>Движение биты игрока.</summary>
    public struct BatDirection : ISideble
    {
        public SideOfConflict Side { get; set; }
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