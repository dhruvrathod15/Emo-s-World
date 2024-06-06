using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    [SerializeField] SuperManager superManager;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.UIElementDictionary,"btnPlayMenuBar").onClick.AddListener(Play);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary,"btnRestartMenuBar").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary,"btnGameModeMenuBar").onClick.AddListener(gameMode);
    }
    private void Play()
    {
        superManager.ButtonClicked.Play();
        pnlMenuBar.SetActive(false);
        pnlGameUI.SetActive(true);
        Time.timeScale = 1.0f;
    }
    void Restart()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("EndlessScene");
        Time.timeScale = 1.0f;
    }
    void gameMode()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("GameModeScene");
        Time.timeScale = 1.0f;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
