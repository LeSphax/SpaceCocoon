using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{

    private string levelName = "Prefabs/Levels/Level";
    private int currentLevelIndex = 1;

    private GameObject currentLevel;

    // Use this for initialization
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadLevel()
    {
        Debug.Log(levelName + currentLevelIndex);
        GameObject levelPrefab = Resources.Load<GameObject>(levelName + currentLevelIndex);
        currentLevel = (GameObject)Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
    }

    public void LoadNextLevel()
    {
        Invoke("_LoadNextLevel", 0.8f);

    }
    void _LoadNextLevel()
    {
        Destroy(currentLevel);
        currentLevelIndex++;
        Invoke("LoadLevel", 0.5f);
    }
}
