using System;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    /// <summary>Получает ввод от пользователя.</summary>
    public class InputService : IInputServiceble
    {
        public UserInput Inputs { get; set; }

        public event EventHandler<BatDirection> BatDirectionEvent;

        public InputService()
        {
            Inputs = new UserInput();
            Subscribe();
        }

        ~InputService()
        {
            Unsubscribing();
        }

        public void Subscribe()
        {
            Inputs.Enable();
            Inputs.PlayerInput.Player1Movement.started += OnMovementFirstPlayer;
            Inputs.PlayerInput.Player1Movement.performed += OnMovementFirstPlayer;
            Inputs.PlayerInput.Player1Movement.canceled += OnMovementFirstPlayer;

            Inputs.PlayerInput.Player2Movement.started += OnMovementSecondPlayer;
            Inputs.PlayerInput.Player2Movement.performed += OnMovementSecondPlayer;
            Inputs.PlayerInput.Player2Movement.canceled += OnMovementSecondPlayer;

            Inputs.PlayerInput.InitialRoll.performed += OnInitialRoll;
        }

        public void Unsubscribing()
        {
            Inputs.PlayerInput.Player1Movement.started -= OnMovementFirstPlayer;
            Inputs.PlayerInput.Player1Movement.performed -= OnMovementFirstPlayer;
            Inputs.PlayerInput.Player1Movement.canceled -= OnMovementFirstPlayer;

            Inputs.PlayerInput.Player2Movement.started -= OnMovementSecondPlayer;
            Inputs.PlayerInput.Player2Movement.performed -= OnMovementSecondPlayer;
            Inputs.PlayerInput.Player2Movement.canceled -= OnMovementSecondPlayer;

            Inputs.PlayerInput.InitialRoll.performed -= OnInitialRoll;

            Inputs.Disable();
            Inputs.Dispose();
        }

        public void OnMovementFirstPlayer(CallbackContext context)
        {
            UnityEngine.Vector2 destination = context.performed
                ? context.ReadValue<UnityEngine.Vector2>()
                : UnityEngine.Vector2.zero;

            GetBitDirectionEvent(SideOfConflict.First, destination, false);
        }

        public void OnMovementSecondPlayer(CallbackContext context)
        {
            UnityEngine.Vector2 destination = context.performed
                ? context.ReadValue<UnityEngine.Vector2>()
                : UnityEngine.Vector2.zero;

            GetBitDirectionEvent(SideOfConflict.Second, destination, false);
        }

        public void OnInitialRoll(CallbackContext context)
        {
            GetBitDirectionEvent(SideOfConflict.Second, UnityEngine.Vector2.zero, true);
        }

        /// <summary>Формирует и публикует событие.</summary>
        /// <param name="side">Сторона конфликта.</param>
        /// <param name="destination">Направление движения биты.</param>
        /// <param name="isInitailRoll">Сделан ли первый удар по шару.</param>
        private void GetBitDirectionEvent(SideOfConflict side, UnityEngine.Vector2 destination, bool isInitailRoll)
        {
            BatDirection batDirection = isInitailRoll
                ? new BatDirection(side, isInitailRoll)
                : new BatDirection(side, destination);

            BatDirectionEvent?.Invoke(this, batDirection);
        }
    }
}