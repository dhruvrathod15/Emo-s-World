using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TimerGameOver : MonoBehaviour
{

    [SerializeField] SuperManagerTimer superManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnHomeGameOver").onClick.AddListener(Home);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnGameOverRestart").onClick.AddListener(Restart);
    }
    private void Home()
    {
        superManager.ButtonClicked.Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("HomeScene");
        pnlGameUI.SetActive(false);
        pnlGameOver.SetActive(false);
    }
    private void Restart()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameUI.SetActive(true);
        pnlGameOver.SetActive(false);
        SceneManager.LoadScene("TimerScene");
        FindObjectOfType<TimerCoinManager>().ResetCoins();
        Time.timeScale = 1.0f;
    }

    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
