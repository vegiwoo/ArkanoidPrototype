using System;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    /// <summary>Глобальный сервис игры.</summary>
    public interface IGameServiceble
    {
        /// <summary>Выход из игры.</summary>
        public void ExitGame();
    }

    /// <summary>Сервис рабты со сценами.</summary>
    public interface ISceneble
    {
        /// <summary>Загружает сцену по имени.</summary>
        /// <param name="scene">Имя сцены.</param>
        void LoadScene(Scene scene);
    }

    /// <summary>Сервис для работы с настройками игры.</summary>
    public interface ISettingServiceble
    {
        /// <summary>Возвращает текущние настройки игры.</summary>
        /// <returns>Текущие настройки.</returns>
        GameSettings GetGameSettings();

        /// <summary>Устанавливает настройки игры.</summary>
        /// <param name="settings">Переданные настройки.</param>
        void SetGameSettings(GameSettings settings);

        /// <summary>Сохраняет хранилище.</summary>
        void SaveStorage();
    }

    /// <summary>Подписка и отписка от интересующих событий.</summary>
    public interface ISubscribing
    {
        /// <summary>Подписывается на все интересующие события.</summary>
        void Subscribe();

        /// <summary>Отписывется от всех интересующих событий.</summary>
        void Unsubscribing();
    }

    /// <summary>Получение ввода от игрока.</summary>
    public interface IInputServiceble : ISubscribing
    {
        /// <summary>Класс реалиазции пользовательского ввода.</summary>
        UserInput Inputs { get; set; }

        /// <summary>Обработчик события изменения направления движения биты определенного игрока.</summary>
        event EventHandler<BatDirection> BatDirectionEvent;

        /// <summary>Получение ввода от первого игрока.</summary>
        /// <param name="context">Контекст ввода.</param>
        void OnMovementFirstPlayer(CallbackContext context);

        /// <summary>Получение ввода от второго игрока.</summary>
        /// <param name="context">Контекст ввода.</param>
        void OnMovementSecondPlayer(CallbackContext context);

        /// <summary>Начальное отбивание шарика, когда он находится на какой-либо платформе (бите).</summary>
        /// <param name="context"></param>
        void OnInitialRoll(CallbackContext context);
    }

    /// <summary>Требует определения стороны конфликта.</summary>
    public interface ISideble
    {
        SideOfConflict Side { get; set; }
    }
}