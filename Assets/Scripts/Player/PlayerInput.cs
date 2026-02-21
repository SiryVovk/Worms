using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActionsMap;

public class PlayerInput : MonoBehaviour, IPlayerActions
{
    public Action<float> OnMoveAction;
    public Action OnJumpAction;
    public Action OnInventory;

    private PlayerActionsMap playerActionsMap;

    private float movementInput;

    private void Awake()
    {
        playerActionsMap = new PlayerActionsMap();
        playerActionsMap.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        playerActionsMap.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsMap.Player.Disable();
    }

    public void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
        OnMoveAction?.Invoke(movementInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnJumpAction?.Invoke();
        }
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInventory?.Invoke();
        }
    }

    public void OnWeaponDirection(InputAction.CallbackContext context)
    {

    }
}
