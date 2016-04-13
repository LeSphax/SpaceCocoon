using UnityEngine;
using System.Collections.Generic;
using System;

public class BoardModel : MonoBehaviour
{

    public float sizeX;
    public float sizeY;

    public int numberTileX;
    public int numberTileY;

    public GameObject tilePrefab;

    public GameObject[] tiles;
    public GameObject[] bricks;

    private BoardPosition[,] board;

    private Stack<Stack<ICommand>> history;
    private Stack<ICommand> currentTurnCommands;


    private InputManager inputManager;

    internal void CancelLastMovement()
    {
        if (history.Count > 0)
        {
            Stack<ICommand> lastTurnCommands = history.Pop();
            while (lastTurnCommands.Count > 0)
            {
                lastTurnCommands.Pop().Undo();
            }
        }
    }

    internal void Reset()
    {
        while (history.Count > 0)
        {
            CancelLastMovement();
        }
    }


    void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<InputManager>();
    }

    internal TileModel getTile(int posX, int posY)
    {
        return board[posX, posY].tile;
    }


    // Use this for initialization
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        board = new BoardPosition[numberTileX, numberTileY];
        history = new Stack<Stack<ICommand>>();
        for (int x = 0; x < numberTileX; x++)
        {
            for (int y = 0; y < numberTileY; y++)
            {
                Vector3 position = GetWorldPosition(x, y);
                TileModel tile = null;
                BrickModel brick = null;
                int index = y + x * numberTileX;
                if (tiles[index] == null)
                {
                    tile = ((GameObject)Instantiate(tilePrefab, position, Quaternion.identity)).GetComponent<TileModel>();
                }
                else
                {
                    tile = ((GameObject)Instantiate(tiles[index], position, Quaternion.identity)).GetComponent<TileModel>();
                }
                if (bricks[index] != null)
                {
                    brick = ((GameObject)Instantiate(bricks[index], position, Quaternion.identity)).GetComponent<BrickModel>();
                    brick.Init(x, y, this);
                }
                board[x, y] = new BoardPosition(tile, brick);
            }
        }
    }


    internal void MoveBrick(int posX, int posY, int newPosX, int newPosY)
    {
        if (IsTileEmpty(newPosX, newPosY))
        {
            board[newPosX, newPosY].brick = board[posX, posY].brick;
            board[posX, posY].brick = null;
        }
        else
        {
            Debug.LogError("BoardModel.MoveBrick(" + posX + "," + posY + "," + newPosX + "," + newPosY + ") : The target tile should be empty");
        }
    }

    internal Vector3 GetWorldPosition(int posX, int posY)
    {
        return new Vector3(posX * sizeX, 0, posY * sizeY);
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.Direction direction = inputManager.GetDirection();
        if (direction != InputManager.Direction.NONE)
        {
            currentTurnCommands = new Stack<ICommand>();
            MoveBricks(direction);
            history.Push(currentTurnCommands);
        }

    }

    private void MoveBricks(InputManager.Direction direction)
    {
        if (direction == InputManager.Direction.UP)
        {
            for (int y = numberTileY - 1; y >= 0; y--)
            {
                for (int x = 0; x < numberTileY; x++)
                {
                    MoveBrick(direction, x, y);
                }
            }
        }
        else if (direction == InputManager.Direction.DOWN)
        {
            for (int y = 0; y < numberTileY; y++)
            {
                for (int x = 0; x < numberTileY; x++)
                {
                    MoveBrick(direction, x, y);
                }
            }
        }
        else if (direction == InputManager.Direction.RIGHT)
        {
            for (int x = numberTileX - 1; x >= 0; x--)
            {
                for (int y = 0; y < numberTileY; y++)
                {
                    MoveBrick(direction, x, y);
                }
            }
        }
        else if (direction == InputManager.Direction.LEFT)
        {
            for (int x = 0; x < numberTileX; x++)
            {
                for (int y = 0; y < numberTileY; y++)
                {

                    MoveBrick(direction, x, y);
                }
            }
        }
    }

    private void MoveBrick(InputManager.Direction direction, int y, int x)
    {
        if (!IsTileEmpty(x, y))
        {
            BrickMoveCommand moveCommand = new BrickMoveCommand(board[x, y].brick, direction);
            moveCommand.Execute();
            if (moveCommand.WasUseful())
            {
                currentTurnCommands.Push(moveCommand);
            }
        }
    }

    internal void RemoveBrick(int x, int y)
    {
        board[x, y].brick = null;
    }

    internal void AddBrick(BrickModel brickModel, int x, int y)
    {
        board[x, y].brick = brickModel;
    }

    internal bool IsTileEmpty(int x, int y)
    {
        return board[x, y].brick == null;
    }

    internal void printBoard()
    {
        string message = "";
        for (int y = 0; y < numberTileY; y++)
        {
            for (int x = 0; x < numberTileY; x++)
            {
                message += board[x, y].brick.objectiveType + ",";
            }
            message += "\n";
        }
        Debug.Log(message);
    }

    public class BoardPosition
    {
        public TileModel tile;
        public BrickModel brick;

        public BoardPosition(TileModel tile, BrickModel brick)
        {
            this.tile = tile;
            this.brick = brick;
        }

        public override string ToString()
        {
            return (brick == null).ToString();
        }
    }

}
