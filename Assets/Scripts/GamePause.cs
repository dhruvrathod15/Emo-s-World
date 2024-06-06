using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] SuperManager superManager;
    [SerializeField] GameObject pnlPauseMenu;
    [SerializeField] GameObject pnlMenuBar;
    [SerializeField] GameObject pnlGameUI;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnplay").onClick.AddListener(gamePlay);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnpause").onClick.AddListener(gamePause);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary,"btnrestart").onClick.AddListener(Restart);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnmenubar").onClick.AddListener(MenuBar);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnhome").onClick.AddListener(Home);
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
        pnlMenuBar.SetActive(false); // Ensure that the menu bar panel is also deactivated

        // Reset time scale to normal
        Time.timeScale = 1.0f;

        // Load the GameScene
        SceneManager.LoadScene("EndlessScene");
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
        SceneManager.LoadScene("HomeScene");
        Time.timeScale = 1.0f;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
