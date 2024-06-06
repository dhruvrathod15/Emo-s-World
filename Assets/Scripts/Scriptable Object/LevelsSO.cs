using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelsSO", menuName = "ScriptableObjects/LevelsSO")]
public class LevelsSO : SerializedScriptableObject
{
    public Dictionary<string, GameObject> levelPrefabDictionary;
    public Dictionary<string, GameObject> levelButtonsDictionary;
    public Dictionary<string, Vector3> startPointPositions;
    public Image Lock;
    public Image Unlock;

    public GameObject GetLevelButton(string levelName)
    {
        GameObject levelButtonPrefab = null;

        // Check if the level button dictionary contains the specified level
        if (levelButtonsDictionary.ContainsKey(levelName))
        {
            levelButtonPrefab = levelButtonsDictionary[levelName];
        }
        return levelButtonPrefab;
    }
    public GameObject GetLevelPrefab(string levelName)
    {
        GameObject levelPrefab = null;

        // Check if the level prefab dictionary contains the specified level
        if (levelPrefabDictionary.ContainsKey(levelName))
        {
            levelPrefab = levelPrefabDictionary[levelName];
        }
        return levelPrefab;
    }
}
