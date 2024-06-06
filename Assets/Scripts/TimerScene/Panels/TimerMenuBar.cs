using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerMenuBar : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnPlayMenuBar").onClick.AddListener(Play);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnRestartMenuBar").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnGameModeMenuBar").onClick.AddListener(gameMode);
    }
    private void Play()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlMenuBar.SetActive(false);
        pnlGameUI.SetActive(true);
        Time.timeScale = 1.0f;
    }
    private void Restart()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameUI.SetActive(true);
        pnlMenuBar.SetActive(false);
        SceneManager.LoadScene("TimerScene");
        PlayerControllerTimer playerController = FindObjectOfType<PlayerControllerTimer>();
        if (playerController != null)
        {
            playerController.ResetCoins();
        }
        Time.timeScale = 1.0f;
    }
    void gameMode()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        SceneManager.LoadScene("GameModeScene");
        Time.timeScale = 1.0f;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
