using UnityEngine;
using UnityEngine.InputSystem;

public class S_Inputs : MonoBehaviour
{
    public Vector2 moveDirection;
    public bool pause;

    //basic Move Input with unity new input system
    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    //Basic pause input
    private void OnPause(InputValue value)
    {
        pause = value.isPressed;
    }
}
