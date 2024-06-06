using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverLevelsMode : MonoBehaviour
{
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        superManager.GamePlay.Stop();
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnHomeGameOver").onClick.AddListener(Home);
        GetButtonComponentFromDictionary(superManager.levelUIElementDictionary, "btnGameOverRestart").onClick.AddListener(Restart);
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
        FindObjectOfType<LevelSelection>().RestartLevel();
        FindObjectOfType<PlayerControllerForLevels>().RestPosition();
        FindObjectOfType<PlayerControllerForLevels>().ResetHealth();
        FindObjectOfType<PlayerControllerForLevels>().ResetCoins();

        pnlGameOver.SetActive(false);
        pnlGameUI.SetActive(true);
        Time.timeScale = 1.0f;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
