using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnTeleportActionPressed = new UnityEvent();
    public UnityEvent OnPauseActionPressed = new UnityEvent();

    [SerializeField]
    private InputActionAsset inputActionAsset = null;

    private InputAction teleportAction = null;
    private InputAction pauseAction = null;

    private void Awake()
    {
        InputActionMap teleportationActionMap = inputActionAsset.FindActionMap("GameActions");

        teleportAction = teleportationActionMap.FindAction("Teleport");
        pauseAction = teleportationActionMap.FindAction("Pause");
    }

    private void OnEnable()
    {
        teleportAction.Enable();
        teleportAction.performed += TeleportActionPressed;
        pauseAction.Enable();
        pauseAction.performed += PauseActionPressed;
    }

    private void OnDisable()
    {
        teleportAction.performed -= TeleportActionPressed;
        teleportAction.Disable();
        pauseAction.performed -= PauseActionPressed;
        pauseAction.Disable();
    }

    private void TeleportActionPressed(InputAction.CallbackContext callbackContext)
    {
        OnTeleportActionPressed.Invoke();
    }

    private void PauseActionPressed(InputAction.CallbackContext callbackContext)
    {
        OnPauseActionPressed.Invoke();
    }
}
