using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] SuperManager superManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.UIElementDictionary,"btnHome").onClick.AddListener(Home);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnGameOverRestart").onClick.AddListener(Restart);

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
        SceneManager.LoadScene("EndlessScene");
        pnlGameUI.SetActive(true);
        Time.timeScale = 1.0f;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}

