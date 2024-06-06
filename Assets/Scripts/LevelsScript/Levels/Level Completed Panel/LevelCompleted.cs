using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] ParticleSystem UIParticleSystem;
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] GameObject pnlLevelCompleted;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] LevelSelection levelSelection;


    private void Awake()
    {
        UIParticleSystem.Play();
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnLevelCompletedHome").onClick.AddListener(Home);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnNextLevel").onClick.AddListener(NextLevel);
    }

    void Home()
    {
        superManager.ButtonClicked.Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("HomeScene");
        pnlLevelCompleted.SetActive(false);
        pnlGameUI.SetActive(false);
    }

    void NextLevel()
    {
        superManager.ButtonClicked.Play();
        Time.timeScale = 1.0f;
        pnlLevelCompleted.SetActive(false);
        levelSelection.UnlockNextLevel();
        levelSelection.SaveCurrentLevelIndex();
        levelSelection.LoadNextLevel();
        FindObjectOfType<PlayerControllerForLevels>().RestPosition();
        FindObjectOfType<PlayerControllerForLevels>().ResetHealth();
        FindObjectOfType<PlayerControllerForLevels>().ResetCoins();
        pnlGameUI.SetActive(true);
    }

    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
