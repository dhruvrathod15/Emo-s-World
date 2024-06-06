using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] GameObject pnlPauseMenu;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnPausePlay").onClick.AddListener(gamePlay);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnPause").onClick.AddListener(gamePause);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnrestart").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnmenubar").onClick.AddListener(MenuBar);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnPauseHome").onClick.AddListener(Home);
    }

    private void gamePause()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        pnlPauseMenu.SetActive(true);
        pnlGameUI.SetActive(false);
        Time.timeScale = 0.0f;
    }

    private void gamePlay()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameUI.SetActive(true);
        pnlPauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void Restart()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameUI.SetActive(true);
        pnlPauseMenu.SetActive(false);
        pnlMenuBar.SetActive(false);
        Time.timeScale = 1.0f;
        FindObjectOfType<LevelSelection>().RestartLevel();
        FindObjectOfType<PlayerControllerForLevels>().RestPosition();
        FindObjectOfType<PlayerControllerForLevels>().ResetHealth();
        FindObjectOfType<PlayerControllerForLevels>().ResetCoins();
    }

    private void MenuBar()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        pnlGameUI.SetActive(false);
        pnlPauseMenu.SetActive(false);
        pnlMenuBar.SetActive(true);
    }

    void Home()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        SceneManager.LoadScene("HomeScene");
        Time.timeScale = 1;
    }

    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
