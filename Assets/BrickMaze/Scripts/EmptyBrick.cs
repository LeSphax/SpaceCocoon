using System;
using UnityEngine;

class EmptyBrick : IBrickModel
{

    private int posX;
    private int posY;

    private BoardModel boardModel;

    public TileModel.Type GetObjectiveType()
    {
        return TileModel.Type.NONE;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(posX, posY);
    }

    public void Init(int x, int y, BoardModel model)
    {
        this.boardModel = model;
        this.posX = x;
        this.posY = y;
    }

    public bool IsEmpty()
    {
        return true;
    }

    public void Move(InputManager.Direction direction)
    {
    }

    public void MoveBack(int oldPosX, int oldPosY)
    {
    }

    public override string ToString()
    {
        return "EmptyBrick";
    }
}
