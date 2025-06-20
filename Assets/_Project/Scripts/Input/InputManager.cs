using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlSchemeType { KeyboardMouse, Gamepad_Xbox, Gamepad_PS, Gamepad_Generic }

public class InputManager : MonoBehaviour
{
    public static event Action<ControlSchemeType> OnControlSchemeChanged;
    
    public GameInput InputActions { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public ControlSchemeType CurrentScheme { get; private set; }

    private void Awake()
    {
        InputActions = new GameInput();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void OnDestroy()
    {
        PlayerInput.onControlsChanged -= OnControlsChanged;
    }
    
    private void OnControlsChanged(PlayerInput playerInput)
    {
        string schemeName = playerInput.currentControlScheme;
        var lastUsedDevice = playerInput.devices.Count > 0 ? playerInput.devices[playerInput.devices.Count - 1] : null;

        if (schemeName == "Keyboard&Mouse")
        {
            CurrentScheme = ControlSchemeType.KeyboardMouse;
        }
        else if (schemeName == "Gamepad")
        {
            if (lastUsedDevice is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            {
                CurrentScheme = ControlSchemeType.Gamepad_PS;
            }
            else if (lastUsedDevice is UnityEngine.InputSystem.XInput.XInputController)
            {
                CurrentScheme = ControlSchemeType.Gamepad_Xbox;
            }
            else
            {
                CurrentScheme = ControlSchemeType.Gamepad_Generic;
            }
        }
        
        Debug.Log($"Схема управления изменена на: {CurrentScheme}");
        OnControlSchemeChanged?.Invoke(CurrentScheme);
    }
}