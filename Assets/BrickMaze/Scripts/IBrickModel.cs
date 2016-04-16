using UnityEngine;
interface IBrickModel
{
    void Move(InputManager.Direction direction);

    void MoveBack(int oldPosX, int oldPosY);
    bool IsEmpty();

    void Init(int x, int y, BoardModel model);

    Vector2 GetPosition();

    TileModel.Type GetObjectiveType();
}

