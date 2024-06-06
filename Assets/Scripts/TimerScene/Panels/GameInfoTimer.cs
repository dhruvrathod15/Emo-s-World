using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameInfoTimer : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    [SerializeField] GameObject panelGameUI;
    [SerializeField] GameObject panelGameInfo;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnInfoTimer").onClick.AddListener(GameInfo);
        GetButtonComponentFromDictionary(superManager.timerUIElementDictionary, "btnBackTimer").onClick.AddListener(Back);
    }

    private void GameInfo()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        panelGameInfo.SetActive(true);
        panelGameUI.SetActive(false);
    }

    private void Back()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        panelGameInfo.SetActive(false);
        panelGameUI.SetActive(true);
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}