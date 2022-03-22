namespace Arkanoid
{
    /// <summary>Ключи для сохранени параметров</summary>
    public enum KeyForSavingParams
    {
        GameMode, IsMuteSound, CurrentSoundVolumeLevel
    }

    /// <summary>Команды кнопок MainMenu</summary>
    public enum MainMenuCommand { NewGame, Settings, Exit };

    /// <summary>Режим сложности игры.</summary>
    public enum DifficultyGameMode { Easy = 0, Medium = 1, Hard = 2}

    /// <summary>Сторона конфликта.</summary>
    public enum SideOfConflict { First = 1, Second = 2 }

    /// <summary>Изменение скорости мяча.</summary>
    public enum ChangeBallSpeed {
        Initial, // Начальная скорость  
        Up,      // Повышение скорости 
    }
}