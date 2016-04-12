using UnityEngine;
using System.Collections;
using System;

public class BoardModel : MonoBehaviour
{

    public float sizeX;
    public float sizeY;

    public int numberTileX;
    public int numberTileY;

    public GameObject tilePrefab;
    public GameObject brickPrefab;
    public GameObject wallPrefab;

    public GameObject[] tiles;
    public GameObject[] bricks;

    private BoardPosition[,] board;

    private InputManager inputManager;


    void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<InputManager>();
    }


    // Use this for initialization
    void Start()
    {
        board = new BoardPosition[numberTileX, numberTileY];
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
        //printBoard();
        if (IsTileEmpty(newPosX, newPosY))
        {
            board[newPosX, newPosY].brick = board[posX, posY].brick;
            board[posX, posY].brick = null;
        }
        else
        {
            Debug.LogError("BoardModel.MoveBrick(" + posX + "," + posY + "," + newPosX + "," + newPosY + ") : The target tile should be empty");
        }
        Debug.Log(board[newPosX, newPosY].tile.getType());
        Debug.Log(board[newPosX, newPosY].brick.objectiveType);
        if (board[newPosX,newPosY].tile.getType() == board[newPosX, newPosY].brick.objectiveType)
        {
            board[newPosX, newPosY].brick.FillObjective();
            board[newPosX, newPosY].tile.SetType(TileModel.Type.FILLED_OBJECTIVE);
        }
       // printBoard();
    }

    internal Vector3 GetWorldPosition(int posX, int posY)
    {
        return new Vector3(posX * sizeX, 0, posY * sizeY);
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.Direction direction = inputManager.GetDirection();
        if (direction == InputManager.Direction.UP)
        {
            for (int y = numberTileY - 1; y >= 0; y--)
            {
                for (int x = 0; x < numberTileY; x++)
                {
                    if (!IsTileEmpty(x, y))
                        board[x, y].brick.Move(direction);
                }
            }
        }
        else if (direction == InputManager.Direction.DOWN)
        {
            for (int y = 0; y < numberTileY; y++)
            {
                for (int x = 0; x < numberTileY; x++)
                {
                    if (!IsTileEmpty(x, y))
                        board[x, y].brick.Move(direction);
                }
            }
        }
        else if (direction == InputManager.Direction.RIGHT)
        {
            for (int x = numberTileX - 1; x >= 0; x--)
            {
                for (int y = 0; y < numberTileY; y++)
                {
                    if (!IsTileEmpty(x, y))
                        board[x, y].brick.Move(direction);
                }
            }
        }
        else if (direction == InputManager.Direction.LEFT)
        {
            for (int x = 0; x < numberTileX; x++)
            {
                for (int y = 0; y < numberTileY; y++)
                {
                    if (!IsTileEmpty(x, y))
                        board[x, y].brick.Move(direction);
                }
            }
        }

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

    public class BoardPositionArray
    {
        public BoardPosition[] array;
    }
}
