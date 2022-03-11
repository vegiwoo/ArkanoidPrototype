using System;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    /// <summary>Получает ввод от игрока.</summary>
    public class InputController : IInputable
    {
        public UserInput Inputs { get; set; }

        public event EventHandler<BatDirection> BatDirectionEvent;

        public InputController()
        {
            Inputs = new UserInput();
            Subscribe();
        }

        ~InputController()
        {
            Unsubscribing();
        }

        public void Subscribe()
        {
            Inputs.Enable();
            Inputs.PlayerInput.Player1Movement.started += OnMovementFirstPalyer;
            Inputs.PlayerInput.Player1Movement.performed += OnMovementFirstPalyer;
            Inputs.PlayerInput.Player1Movement.canceled += OnMovementFirstPalyer;

            Inputs.PlayerInput.Player2Movement.started += OnMovementSecondPalyer;
            Inputs.PlayerInput.Player2Movement.performed += OnMovementSecondPalyer;
            Inputs.PlayerInput.Player2Movement.canceled += OnMovementSecondPalyer;
        }

        public void Unsubscribing()
        {
            Inputs.PlayerInput.Player1Movement.started -= OnMovementFirstPalyer;
            Inputs.PlayerInput.Player1Movement.performed -= OnMovementFirstPalyer;
            Inputs.PlayerInput.Player1Movement.canceled -= OnMovementFirstPalyer;

            Inputs.PlayerInput.Player2Movement.started -= OnMovementSecondPalyer;
            Inputs.PlayerInput.Player2Movement.performed -= OnMovementSecondPalyer;
            Inputs.PlayerInput.Player2Movement.canceled -= OnMovementSecondPalyer;

            Inputs.Disable();
            Inputs.Dispose();
        }

        public void OnMovementFirstPalyer(CallbackContext context)
        {
            if (context.performed)
            {
                UnityEngine.Vector2 destination = context.ReadValue<UnityEngine.Vector2>();
                GetBitDirectionEvent(SideOfConflict.First, destination);
            }
            else
            {
                GetBitDirectionEvent(SideOfConflict.First, UnityEngine.Vector2.zero);
            }
        }

        public void OnMovementSecondPalyer(CallbackContext context)
        {
           if (context.performed)
            {
                UnityEngine.Vector2 destination = context.ReadValue<UnityEngine.Vector2>();
                GetBitDirectionEvent(SideOfConflict.Second, destination);
            }
            else
            {
                GetBitDirectionEvent(SideOfConflict.Second, UnityEngine.Vector2.zero);
            }
        }

        private void GetBitDirectionEvent(SideOfConflict side, UnityEngine.Vector2 destination)
        {
            BatDirection bitDirection = new BatDirection(side, destination);
            BatDirectionEvent?.Invoke(this, bitDirection);
        }
    }
}