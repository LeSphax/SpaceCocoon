using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int startingLevel = 1;
    public int lastLevel = 10;
    private string levelName = "Prefabs/Levels/Level";
    private int currentLevelIndex = 1;

    private GameObject currentLevel;
    private InputManager inputManager;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Start()
    {
            currentLevelIndex = startingLevel;
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadLevel()
    {
        GameObject levelPrefab = Resources.Load<GameObject>(levelName + currentLevelIndex);
        currentLevel = (GameObject)Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        inputManager.SetBoard(currentLevel.GetComponent<BoardModel>());
    }

    public void LoadNextLevel()
    {
        Invoke("_LoadNextLevel", 0.5f);

    }
    void _LoadNextLevel()
    {
        Destroy(currentLevel);
        currentLevelIndex++;
        if (currentLevelIndex > 10)
        {
            currentLevelIndex = 1;
        }
        Invoke("LoadLevel", 0.3f);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
