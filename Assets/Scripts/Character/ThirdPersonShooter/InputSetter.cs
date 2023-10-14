using UnityEngine;
using UnityEngine.InputSystem;

public class InputSetter : MonoBehaviour
{
    [SerializeField] private CharacterInputProvider _inputProvider;
    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        MoveInput(value);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
         if(_inputProvider.cursorInputForLook)
         {
           LookInput(value);
         }
    }
        
    public void OnJump(InputAction.CallbackContext context)
    {
        var value = context.ReadValueAsButton();
        JumpInput(value);
    }
        
    public void OnSprint(InputAction.CallbackContext context)
    {
        var value = context.ReadValueAsButton();
        SprintInput(value);
    }
    
    protected virtual void MoveInput(Vector2 newMoveDirection)
    {
        _inputProvider.move = newMoveDirection;
    } 

    protected virtual void LookInput(Vector2 newLookDirection)
    {
        _inputProvider.look = newLookDirection;
    }

    protected virtual void JumpInput(bool newJumpState)
    {
        _inputProvider.jump = newJumpState;
    }

    protected virtual void SprintInput(bool newSprintState)
    {
        _inputProvider.sprint = newSprintState;
    }
    
    

}
