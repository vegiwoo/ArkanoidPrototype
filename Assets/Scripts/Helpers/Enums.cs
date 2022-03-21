namespace Arkanoid
{
    /// <summary>Режим сложности игры.</summary>
    public enum DifficultyGameMode
    {
        Easy, Medium, Hard
    }

    /// <summary>Сторона конфликта.</summary>
    public enum SideOfConflict { First = 1, Second = 2 }

    /// <summary>Изменение скорости мяча.</summary>
    public enum ChangeBallSpeed {
        Initial, // Начальная скорость  
        Up,      // Повышение скорости 
    }
}