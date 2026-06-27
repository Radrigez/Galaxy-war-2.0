using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance {get; private set;}
    public PlayerInputActions playerInputActions;
    void Awake()
    {
        instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }

    public Vector2 GetMovementVector()
    {
        Vector2 movementVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return movementVector;
    }
    
}
