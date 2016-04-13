﻿using UnityEngine;
class BrickMoveCommand : ICommand
{
    private BrickModel brick;
    private InputManager.Direction direction;

    private Vector2 oldBrickPosition;
    private Vector2 newBrickPosition;

    public BrickMoveCommand(BrickModel brick, InputManager.Direction direction)
    {
        this.brick = brick;
        this.direction = direction;
    }

    public void Execute()
    {
        oldBrickPosition = brick.getPosition();
        brick.Move(direction);
        newBrickPosition = brick.getPosition();
    }

    public void Undo()
    {
        if (oldBrickPosition != newBrickPosition)
            brick.MoveBack((int)oldBrickPosition.x, (int)oldBrickPosition.y);
    }

    public bool WasUseful()
    {
        return oldBrickPosition != newBrickPosition;
    }
}

