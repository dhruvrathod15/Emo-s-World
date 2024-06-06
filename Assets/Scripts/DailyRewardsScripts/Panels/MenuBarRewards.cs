using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuBarRewards : MonoBehaviour
{
    [SerializeField] SuperManagerDailyRewards superManager;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnPlayMenuBar").onClick.AddListener(Play);
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnGameModeMenuBar").onClick.AddListener(gameMode);
    }
    private void Play()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlMenuBar.SetActive(false);
        pnlGameUI.SetActive(true);
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