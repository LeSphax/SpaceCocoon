using System;
using System.Collections.Generic;
using UnityEngine;

public class BrickModel : MonoBehaviour, IBrickModel
{


    private int posX;
    private int posY;

    private BoardModel boardModel;
    private BrickView view;

    private bool fusionned = false;

    public Vector2 GetPosition()
    {
        return new Vector2(posX, posY);
    }

    private Queue<Vector3> targetPositions;

    public TileModel.Type objectiveType = TileModel.Type.NONE;

    public TileModel.Type GetObjectiveType()
    {
        return objectiveType;
    }



    void Awake()
    {
        targetPositions = new Queue<Vector3>();
    }

    public void Init(int x, int y, BoardModel boardModel)
    {
        this.boardModel = boardModel;
        gameObject.transform.SetParent(boardModel.transform, false);
        view = gameObject.GetComponent<BrickView>();
        //
        //
        this.posX = x;
        this.posY = y;
        view.SetPosition(GetWorldPosition());
    }

    public void SetPosition(int newPosX, int newPosY)
    {
        boardModel.MoveBrick(posX, posY, newPosX, newPosY);
        this.posX = newPosX;
        this.posY = newPosY;
        if (boardModel.getTile(newPosX, newPosY).getType() == objectiveType)
        {
            FillObjective();
        }
        targetPositions.Enqueue(GetWorldPosition());
        Debug.Log("SetPosition " + targetPositions.Count);
        view.SetTarget(targetPositions.Peek());
    }

    private Vector3 GetWorldPosition()
    {
        return boardModel.GetWorldPosition(posX, posY);
    }

    public virtual void Move(InputManager.Direction direction)
    {
        Vector2 newBoardTarget = new Vector2(posX, posY);
        if (direction == InputManager.Direction.UP)
        {
            for (int i = posY + 1; i < boardModel.numberTileY; i++)
            {
                if (boardModel.IsTileEmpty(posX, i))
                {
                    newBoardTarget = new Vector2(posX, i);
                }
                else
                {
                    break;
                }
            }
        }
        else if (direction == InputManager.Direction.DOWN)
        {
            for (int i = posY - 1; i >= 0; i--)
            {
                if (boardModel.IsTileEmpty(posX, i))
                {
                    newBoardTarget = new Vector2(posX, i);
                }
                else
                {
                    break;
                }
            }
        }
        else if (direction == InputManager.Direction.RIGHT)
        {
            for (int i = posX + 1; i < boardModel.numberTileX; i++)
            {
                if (boardModel.IsTileEmpty(i, posY))
                {
                    newBoardTarget = new Vector2(i, posY);
                }
                else
                {
                    break;
                }
            }
        }
        else if (direction == InputManager.Direction.LEFT)
        {
            for (int i = posX - 1; i >= 0; i--)
            {
                if (boardModel.IsTileEmpty(i, posY))
                {
                    newBoardTarget = new Vector2(i, posY);
                }
                else
                {
                    break;
                }
            }
        }
        if (newBoardTarget.x != posX || newBoardTarget.y != posY)
            SetPosition((int)newBoardTarget.x, (int)newBoardTarget.y);
    }

    public virtual void MoveBack(int oldPosX, int oldPosY)
    {
        if (fusionned)
        {
            UnFillObjective();
        }
        SetPosition(oldPosX, oldPosY);
    }

    internal void ViewReachedTarget()
    {
        Debug.Log("ReachedTarget " + objectiveType);
        targetPositions.Dequeue();
        if (targetPositions.Count > 0)
        {
            view.SetTarget(targetPositions.Peek());
        }
        else if (fusionned)
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    internal void FillObjective()
    {
        boardModel.getTile(posX, posY).SetType(TileModel.Type.FILLED_OBJECTIVE);
        boardModel.RemoveBrick(posX, posY);
        fusionned = true;
    }

    private void UnFillObjective()
    {
        boardModel.getTile(posX, posY).SetType(objectiveType);
        boardModel.AddBrick(this,posX, posY);
        GetComponent<Renderer>().enabled = true;
        fusionned = false;
    }

    public virtual bool IsEmpty()
    {
        return false;
    }

    public override string ToString()
    {
        return "Brick " + objectiveType;
    }

}

