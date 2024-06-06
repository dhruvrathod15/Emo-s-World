using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverRewards : MonoBehaviour
{
    [SerializeField] SuperManagerDailyRewards superManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnHomeGameOver").onClick.AddListener(Home);
    }

    private void Home()
    {
        superManager.ButtonClicked.Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("HomeScene");
        pnlGameUI.SetActive(false);
        pnlGameOver.SetActive(false);
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        return dictionary[key].GetComponent<Button>();
    }
}
