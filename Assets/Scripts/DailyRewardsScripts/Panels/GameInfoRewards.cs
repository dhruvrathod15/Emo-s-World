using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoRewards : MonoBehaviour
{
    [SerializeField] SuperManagerDailyRewards superManager;
    [SerializeField] GameObject panelGameUI;
    [SerializeField] GameObject panelGameInfo;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnInfoRewards").onClick.AddListener(GameInfo);
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnBackRewards").onClick.AddListener(Back);
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