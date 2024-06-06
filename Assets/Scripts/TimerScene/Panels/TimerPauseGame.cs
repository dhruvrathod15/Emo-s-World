using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerPauseGame : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    [SerializeField] GameObject pnlPauseMenu;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnPausePlay").onClick.AddListener(gamePlay);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnPause").onClick.AddListener(gamePause);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnrestart").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnmenubar").onClick.AddListener(MenuBar);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnPauseHome").onClick.AddListener(Home);
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
        SceneManager.LoadScene("TimerScene");
        FindObjectOfType<PlayerControllerTimer>().ResetCoins();
        Time.timeScale = 1.0f;
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
