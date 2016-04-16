using UnityEngine;

class WallBrick : BrickModel
{

    public override void Move(InputManager.Direction direction)
    {
    }

    public override void MoveBack(int oldPosX, int oldPosY)
    {
    }

    public override string ToString()
    {
        return "WallBrick";
    }
}
