using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuBarLevelsMode : MonoBehaviour
{
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] GameObject pnlLevelSelection;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnPlayMenuBar").onClick.AddListener(Play);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnRestartMenuBar").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnGameModeMenuBar").onClick.AddListener(gameMode);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary,"btnMenuBarLevels").onClick.AddListener(levelMenu);
    }
    private void Play()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlMenuBar.SetActive(false);
        pnlGameUI.SetActive(true);
        pnlLevelSelection.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void Restart()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        FindObjectOfType<LevelSelection>().RestartLevel();
        FindObjectOfType<PlayerControllerForLevels>().RestPosition();
        FindObjectOfType<PlayerControllerForLevels>().ResetHealth();
        FindObjectOfType<PlayerControllerForLevels>().ResetCoins();
        pnlMenuBar.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void gameMode()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        SceneManager.LoadScene("GameModeScene");
        Time.timeScale = 1.0f;
    }
    void levelMenu()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        pnlLevelSelection.SetActive(true);
        pnlMenuBar.SetActive(false);
        pnlGameUI.SetActive(false);
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
