using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UnityEvent OnTeleportActionPressed = new UnityEvent();

    [SerializeField]
    private InputActionAsset inputActionAsset = null;

    private InputAction teleportAction = null;

    private void Awake()
    {
        InputActionMap teleportationActionMap = inputActionAsset.FindActionMap("Teleportation");
        teleportAction = teleportationActionMap.FindAction("Teleport");
    }

    private void OnEnable()
    {
        teleportAction.Enable();
        teleportAction.performed += TeleportActionPressed;
    }

    private void OnDisable()
    {
        teleportAction.performed -= TeleportActionPressed;
        teleportAction.Disable();
    }

    private void TeleportActionPressed(InputAction.CallbackContext callbackContext)
    {
        OnTeleportActionPressed.Invoke();
    }
}
