using UnityEngine;

class InputManager : MonoBehaviour
{
    private BoardModel board;

    public enum Direction
    {
        UP, DOWN, RIGHT, LEFT,
        NONE
    }

    void Start()
    {
        board = GetComponent<BoardModel>();
    }

    void Update()
    {
        if (Input.GetButtonDown(InputButtonNames.CANCEL))
        {
            board.CancelLastMovement();
        }
        else if (Input.GetButtonDown(InputButtonNames.RESET))
        {
            board.Reset();
        }
    }

    public Direction GetDirection()
    {
        if (Input.GetButtonDown(InputButtonNames.UP))
        {
            return Direction.UP;
        }
        else if (Input.GetButtonDown(InputButtonNames.DOWN))
        {
            return Direction.DOWN;
        }
        else if (Input.GetButtonDown(InputButtonNames.RIGHT))
        {
            return Direction.RIGHT;
        }
        else if (Input.GetButtonDown(InputButtonNames.LEFT))
        {
            return Direction.LEFT;
        }
        return Direction.NONE;
    }
}

