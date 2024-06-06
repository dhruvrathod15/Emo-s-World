using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] RectTransform levelButtonContainer;
    [SerializeField] LevelsSO levelData;
    [SerializeField] GameObject pnlLevelSelection;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] GameObject Player;
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] CoinManager coinManager;

    private GameObject currentLevel;
    private int unlockedLevels = 1;
    public int CurrentLevelIndex { get; set; }

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnBack").onClick.AddListener(Back);
    }

    private void Start()
    {
        LoadUnlockedLevels();
        LoadCurrentLevelIndex();
        CreateLevelButtons();
    }

    private void LoadUnlockedLevels()
    {
        if (PlayerPrefs.HasKey(superManager.unlockedLevelsKey))
        {
            unlockedLevels = PlayerPrefs.GetInt(superManager.unlockedLevelsKey);
        }
    }

    private void LoadCurrentLevelIndex()
    {
        if (PlayerPrefs.HasKey(superManager.currentLevelIndexKey))
        {
            CurrentLevelIndex = PlayerPrefs.GetInt(superManager.currentLevelIndexKey);
        }
        else
        {
            CurrentLevelIndex = 0;
        }
    }

    public void SaveCurrentLevelIndex()
    {
        PlayerPrefs.SetInt(superManager.currentLevelIndexKey, CurrentLevelIndex);
        PlayerPrefs.Save();
    }

    private void CreateLevelButtons()
    {
        for (int i = 0; i < levelData.levelPrefabDictionary.Count; i++)
        {
            string levelName = "Level " + (i + 1);

            GameObject levelButtonGO = Instantiate(levelData.levelButtonsDictionary[levelName], levelButtonContainer);
            Button levelButton = levelButtonGO.GetComponent<Button>();
            TMP_Text buttonText = levelButtonGO.GetComponentInChildren<TMP_Text>();

            buttonText.text = (i + 1).ToString();

            levelButton.interactable = (i < unlockedLevels);

            int levelIndex = i;
            levelButton.onClick.AddListener(() => OnLevelButtonClicked(levelIndex));
        }
    }

    private void OnLevelButtonClicked(int levelIndex)
    {
        superManager.ButtonClicked.Play();
        LoadLevel(levelIndex);
    }

    public void SaveUnlockedLevels()
    {
        PlayerPrefs.SetInt(superManager.unlockedLevelsKey, unlockedLevels);
        PlayerPrefs.Save();
    }

    public void UnlockNextLevel()
    {
        int nextLevelIndex = CurrentLevelIndex + 1;
        if (nextLevelIndex >= unlockedLevels)
        {
            unlockedLevels = nextLevelIndex + 1;
            SaveUnlockedLevels();
            UpdateButtonInteractability();
        }
    }

    public void LoadLevel(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
        SaveCurrentLevelIndex();
        string levelName = "Level " + (levelIndex + 1);
        if (levelData.levelPrefabDictionary.ContainsKey(levelName))
        {
            GameObject levelPrefab = levelData.levelPrefabDictionary[levelName];
            if (levelPrefab != null)
            {
                DestroyPreviousLevel();
                currentLevel = Instantiate(levelPrefab);
                pnlLevelSelection.SetActive(false);
                coinManager.ResetCoins();
                FindObjectOfType<KeyManagerLevels>().ResetKey();
                Player.SetActive(true);
                superManager.GamePlay.Play();
                pnlGameUI.SetActive(true);
                Time.timeScale = 1.0f;
                Vector3 startPointPosition = levelData.startPointPositions[levelName];
                Player.transform.position = startPointPosition;
            }
        }
    }

    public void DestroyPreviousLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }

    public void UpdateButtonInteractability()
    {
        for (int i = 0; i < levelData.levelPrefabDictionary.Count; i++)
        {
            string levelName = "Level " + (i + 1);
            GameObject levelButtonGO = levelButtonContainer.GetChild(i).gameObject;
            Button levelButton = levelButtonGO.GetComponent<Button>();
            levelButton.interactable = (i < unlockedLevels);
        }
    }

    void Back()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("GameModeScene");
    }

    public void RestartLevel()
    {
        LoadLevel(CurrentLevelIndex);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = CurrentLevelIndex + 1;
        LoadLevel(nextLevelIndex);
    }

    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}