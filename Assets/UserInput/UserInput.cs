// GENERATED AUTOMATICALLY FROM 'Assets/UserInput/UserInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Arkanoid
{
    public class @UserInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @UserInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""UserInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""4a51f626-71c2-4735-b333-3c0ae709df9f"",
            ""actions"": [
                {
                    ""name"": ""Player1Movement"",
                    ""type"": ""Button"",
                    ""id"": ""1d590735-469f-41b4-b6d8-0d16409f5e3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player2Movement"",
                    ""type"": ""Button"",
                    ""id"": ""f12f95e1-ed64-4e29-94d7-a979bf92e523"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""e4106cfa-2b78-45ef-a142-f38078e48745"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e2807e9f-dabd-4193-843d-6d5770498e83"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fe400c6c-dd58-4dc3-917e-8846565a8640"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""668b532b-1c18-4e1d-9b0c-b05833987294"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18cf3d22-dd55-4476-bf2f-fec7b318378e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""2f49223f-699d-4312-a646-d2a4c1b0f5aa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player2Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9e9e3cfb-4cfe-4861-ad27-2f545fa278fa"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5dda3091-fc44-4c04-b4ec-04a6f2349f00"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e774cf04-648b-445f-aa0c-73df9dffd250"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""26bbf537-932d-4c16-a459-ad96e4b51f77"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerInput
            m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
            m_PlayerInput_Player1Movement = m_PlayerInput.FindAction("Player1Movement", throwIfNotFound: true);
            m_PlayerInput_Player2Movement = m_PlayerInput.FindAction("Player2Movement", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // PlayerInput
        private readonly InputActionMap m_PlayerInput;
        private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
        private readonly InputAction m_PlayerInput_Player1Movement;
        private readonly InputAction m_PlayerInput_Player2Movement;
        public struct PlayerInputActions
        {
            private @UserInput m_Wrapper;
            public PlayerInputActions(@UserInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Player1Movement => m_Wrapper.m_PlayerInput_Player1Movement;
            public InputAction @Player2Movement => m_Wrapper.m_PlayerInput_Player2Movement;
            public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerInputActions instance)
            {
                if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
                {
                    @Player1Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer1Movement;
                    @Player1Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer1Movement;
                    @Player1Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer1Movement;
                    @Player2Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer2Movement;
                    @Player2Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer2Movement;
                    @Player2Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayer2Movement;
                }
                m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Player1Movement.started += instance.OnPlayer1Movement;
                    @Player1Movement.performed += instance.OnPlayer1Movement;
                    @Player1Movement.canceled += instance.OnPlayer1Movement;
                    @Player2Movement.started += instance.OnPlayer2Movement;
                    @Player2Movement.performed += instance.OnPlayer2Movement;
                    @Player2Movement.canceled += instance.OnPlayer2Movement;
                }
            }
        }
        public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
        public interface IPlayerInputActions
        {
            void OnPlayer1Movement(InputAction.CallbackContext context);
            void OnPlayer2Movement(InputAction.CallbackContext context);
        }
    }
}
