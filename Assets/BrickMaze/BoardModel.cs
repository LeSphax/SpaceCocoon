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
                GameObject newTile = (GameObject)Instantiate(tilePrefab, position, Quaternion.identity);
                BrickModel brick;
                if (x == 0 && y == 0)
                {
                    Debug.Log("Il faut déterminer comment créer les niveaux et tester de voir si tout fonctionne bien avec plusieurs briques");
                    GameObject brickGameObject = (GameObject)Instantiate(brickPrefab, position + Vector3.up * 0.2f, Quaternion.identity);
                    brick = new BrickModel(x, y, brickGameObject, this);
                }
                else
                {
                    brick = null;
                }
                board[x, y] = new BoardPosition(newTile, brick);
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
        if (direction == InputManager.Direction.UP)
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
        else if (direction == InputManager.Direction.DOWN)
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
        else if (direction == InputManager.Direction.RIGHT)
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
        else if (direction == InputManager.Direction.LEFT)
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

    }

    internal bool IsTileEmpty(int x, int y)
    {
        return board[x, y].brick == null;
    }

    private class BoardPosition
    {
        public GameObject tile;
        public BrickModel brick;

        public BoardPosition(GameObject tile, BrickModel brick)
        {
            this.tile = tile;
            this.brick = brick;
        }
    }
}
