using UnityEngine;

public class InputManager : MonoBehaviour
{
    private BoardModel board;

    public enum Swipe { None, Up, Down, Left, Right };

    public float minSwipeLength = 200f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public static Swipe swipeDirection;

    public enum Direction
    {
        UP, DOWN, RIGHT, LEFT,
        NONE
    }
	
    void Update()
    {
		
        if (Input.GetButtonDown(InputButtonNames.CANCEL) || Input.GetKey(KeyCode.Escape))
        {
            Cancel();
        }
        else if (Input.GetButtonDown(InputButtonNames.RESET))
        {
            Reset();
        }
        else if (GetDirection() != Direction.NONE)
        {
            board.Move(GetDirection());
        }
    }

    public void Reset()
    {
        board.Reset();
    }

    public void Cancel()
    {
        board.CancelLastMovement();
    }

    public Direction DetectMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            // Make sure it was a legit swipe, not a tap
            if (currentSwipe.magnitude < minSwipeLength)
            {
                return Direction.NONE;
            }

            currentSwipe.Normalize();

            // Swipe up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return Direction.UP;
            }
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return Direction.DOWN;
            }
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return Direction.LEFT;
            }
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return Direction.RIGHT;
            }
            else
            {
                return Direction.NONE;
            }
        }
        else
        {
            return Direction.NONE;
        }
    }

    public Direction DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    return Direction.NONE;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return Direction.UP;
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return Direction.DOWN;
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    return Direction.LEFT;
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    return Direction.RIGHT;
                }
                else
                {
                    return Direction.NONE;
                }
            }
            else
            {
                return Direction.NONE;
            }
        }
        else {
            return Direction.NONE;
        }
    }


    public Direction GetDirection()
    {
        if (Input.touchSupported)
            return DetectSwipe();
        else if (GetDirectionWithButtons() != Direction.NONE)
            return GetDirectionWithButtons();
        else
            return DetectMouseSwipe();
    }

    private static Direction GetDirectionWithButtons()
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

    public void SetBoard(BoardModel board)
    {
        this.board = board;
    }
}

