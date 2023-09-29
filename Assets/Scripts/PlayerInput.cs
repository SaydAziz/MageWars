using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void moveInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        controller.getMoveInput(context.ReadValue<Vector2>());
    }

    public void lookInput(InputAction.CallbackContext context)
    {
        controller.getLookInput(context.ReadValue<Vector2>());
    }

}
