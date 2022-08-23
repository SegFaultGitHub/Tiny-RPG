//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/UI Actions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @UIActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UI Actions"",
    ""maps"": [
        {
            ""name"": ""Item Selection"",
            ""id"": ""3f2e9d9c-1e0c-47cd-8d53-25eb742c67f0"",
            ""actions"": [
                {
                    ""name"": ""Keep Item"",
                    ""type"": ""Button"",
                    ""id"": ""7395ae88-96fa-4421-b063-5dc130359f27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Take Item"",
                    ""type"": ""Button"",
                    ""id"": ""45072eaa-9d1d-4958-b6f1-fc5acb424125"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a1ede83c-f5f8-4a71-ab83-cfd1ad2776be"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keep Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f87f2c11-a4a7-4be3-b201-bca5fd89e24b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Take Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""dbb820f4-1c73-4f27-8239-ed05a0273599"",
            ""actions"": [
                {
                    ""name"": ""Create Items"",
                    ""type"": ""Button"",
                    ""id"": ""0566baab-cd20-4d05-98a4-db8f6f96193b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""64479b64-5abd-4dfe-84d2-2f0f395cb238"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Create Items"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Item Selection
        m_ItemSelection = asset.FindActionMap("Item Selection", throwIfNotFound: true);
        m_ItemSelection_KeepItem = m_ItemSelection.FindAction("Keep Item", throwIfNotFound: true);
        m_ItemSelection_TakeItem = m_ItemSelection.FindAction("Take Item", throwIfNotFound: true);
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_CreateItems = m_Debug.FindAction("Create Items", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Item Selection
    private readonly InputActionMap m_ItemSelection;
    private IItemSelectionActions m_ItemSelectionActionsCallbackInterface;
    private readonly InputAction m_ItemSelection_KeepItem;
    private readonly InputAction m_ItemSelection_TakeItem;
    public struct ItemSelectionActions
    {
        private @UIActions m_Wrapper;
        public ItemSelectionActions(@UIActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @KeepItem => m_Wrapper.m_ItemSelection_KeepItem;
        public InputAction @TakeItem => m_Wrapper.m_ItemSelection_TakeItem;
        public InputActionMap Get() { return m_Wrapper.m_ItemSelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ItemSelectionActions set) { return set.Get(); }
        public void SetCallbacks(IItemSelectionActions instance)
        {
            if (m_Wrapper.m_ItemSelectionActionsCallbackInterface != null)
            {
                @KeepItem.started -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnKeepItem;
                @KeepItem.performed -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnKeepItem;
                @KeepItem.canceled -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnKeepItem;
                @TakeItem.started -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnTakeItem;
                @TakeItem.performed -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnTakeItem;
                @TakeItem.canceled -= m_Wrapper.m_ItemSelectionActionsCallbackInterface.OnTakeItem;
            }
            m_Wrapper.m_ItemSelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @KeepItem.started += instance.OnKeepItem;
                @KeepItem.performed += instance.OnKeepItem;
                @KeepItem.canceled += instance.OnKeepItem;
                @TakeItem.started += instance.OnTakeItem;
                @TakeItem.performed += instance.OnTakeItem;
                @TakeItem.canceled += instance.OnTakeItem;
            }
        }
    }
    public ItemSelectionActions @ItemSelection => new ItemSelectionActions(this);

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_CreateItems;
    public struct DebugActions
    {
        private @UIActions m_Wrapper;
        public DebugActions(@UIActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CreateItems => m_Wrapper.m_Debug_CreateItems;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @CreateItems.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnCreateItems;
                @CreateItems.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnCreateItems;
                @CreateItems.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnCreateItems;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CreateItems.started += instance.OnCreateItems;
                @CreateItems.performed += instance.OnCreateItems;
                @CreateItems.canceled += instance.OnCreateItems;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);
    public interface IItemSelectionActions
    {
        void OnKeepItem(InputAction.CallbackContext context);
        void OnTakeItem(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnCreateItems(InputAction.CallbackContext context);
    }
}