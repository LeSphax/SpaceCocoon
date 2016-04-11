using UnityEngine;

class InputManager : MonoBehaviour
{
    public enum Direction
    {
        UP, DOWN, RIGHT, LEFT,
        NONE
    }

    void Start()
    {

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

