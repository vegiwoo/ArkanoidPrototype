using System;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    /// <summary>Подписка и отписка от интересующих событий.</summary>
    public interface ISubscribing
    {
        /// <summary>Подписывается на все интересующие события.</summary>
        void Subscribe();

        /// <summary>Отписывется от всех интересующих событий.</summary>
        void Unsubscribing();
    }

    /// <summary>Получение ввода от игрока.</summary>
    public interface IInputable : ISubscribing
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
}