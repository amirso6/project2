using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static CustomInput;

[CreateAssetMenu(fileName ="Input Channel", menuName = "Channels/Input Channel", order = 0)]
public class InputChannel : ScriptableObject, IPlayerActions
{
    CustomInput customInput;
    private void OnEnable()
    {
        if(customInput == null)
        {
            customInput = new CustomInput();

            customInput.Player.SetCallbacks(this);
            customInput.Enable();
        }
    }

    public Action<Vector2> MoveEvent;
    public Action<Vector2> MouseEvent;
    public Action RunEvent;
    public Action RunCancelledEvent;
    public Action JumpEvent;
    public Action PauseEvent;
    public Action OtherPauseEvent;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        MouseEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RunEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            RunCancelledEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context){
        PauseEvent?.Invoke();
        OtherPauseEvent?.Invoke();
    }
}