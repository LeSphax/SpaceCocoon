using System;
using UnityEngine;

class BrickModel
{

    private int posX;
    private int posY;

    private GameObject gameobject;

    private BoardModel boardModel;
    private BrickView view;

    public BrickModel(int posX, int posY, GameObject gameobject, BoardModel controller)
    {
        Debug.Log("Il faudrait que les briques soient positionnées plus haut pour ne pas rentrer dans les tuiles");
        this.gameobject = gameobject;
        this.boardModel = controller;
        view = gameobject.GetComponent<BrickView>();
        //
        //
        this.posX = posX;
        this.posY = posY;
        view.SetPosition(GetWorldPosition());
    }

    public void SetPosition(int newPosX, int newPosY)
    {
        boardModel.MoveBrick(posX, posY, newPosX, newPosY);
        this.posX = newPosX;
        this.posY = newPosY;
        view.SetTarget(GetWorldPosition());
    }

    private Vector3 GetWorldPosition()
    {
        return boardModel.GetWorldPosition(posX, posY);
    }

    internal void Move(InputManager.Direction direction)
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
            }
        }
        if (newBoardTarget.x != posX || newBoardTarget.y != posY)
            SetPosition((int)newBoardTarget.x, (int)newBoardTarget.y);
    }
}

