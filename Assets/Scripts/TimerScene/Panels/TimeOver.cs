using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeOver : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] GameObject pnlTimeOver;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary,"btnTimerHome").onClick.AddListener(Home);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary,"btnTimerRestart").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary,"btnTimerGameMode").onClick.AddListener(GameMode);
    }
    void Home()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("HomeScene");
        pnlGameUI.SetActive(true);
        pnlTimeOver.SetActive(false);
    }
    void Restart()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameUI.SetActive(true);
        pnlTimeOver.SetActive(false);
        SceneManager.LoadScene("TimerScene");
        FindObjectOfType<PlayerControllerTimer>().ResetCoins();
        Time.timeScale = 1.0f;
    }
    void GameMode()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("GameModeScene");
        pnlGameUI.SetActive(true);
        pnlTimeOver.SetActive(false);
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
