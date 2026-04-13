using System;
using UnityEngine;

public class InputReader : MonoBehaviour, PlayerInput.IPlayerActions
{
    public Vector2 Movement { get; private set; }
    public Vector2 Pointer { get; private set; }
    public bool IsAttack { get; private set; }
    public bool IsShooting { get; private set; }
    public event Action OpenUIAction;

    public event Action<int> SelectItemAction;

    PlayerInput input;

    void Start()
    {
        input = new PlayerInput();
        input.Player.SetCallbacks(this);
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            IsAttack = true;
        }

        if (context.canceled)
        {
            IsAttack = false;
        }
    }

    public void OnOpenUI(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OpenUIAction?.Invoke();
        }
    }

    public void OnPointer(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Pointer = context.ReadValue<Vector2>();
    }

    public void OnUseItem(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            string keyName = context.control.name;

            if (int.TryParse(keyName, out int keyNum))
            {
                int slotIndex = keyNum - 1;
                SelectItemAction?.Invoke(slotIndex);
            }
        }
    }


    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }


    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    public void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    public void OnShooting(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsShooting = true;
        }

        if (context.canceled)
        {
            IsShooting = false;
        }
    }


}
