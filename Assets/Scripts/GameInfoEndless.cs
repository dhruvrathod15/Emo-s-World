using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoEndless : MonoBehaviour
{
    [SerializeField] SuperManager superManager;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] GameObject pnlGameInfo;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnGameInfo").onClick.AddListener(Info);
        GetButtonComponentFromDictionary(superManager.UIElementDictionary, "btnBack").onClick.AddListener(Back);
    }
    void Info()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Stop();
        pnlGameInfo.SetActive(true);
        pnlGameUI.SetActive(false);
    }
    void Back()
    {
        superManager.ButtonClicked.Play();
        superManager.GamePlay.Play();
        pnlGameInfo.SetActive(false);
        pnlGameUI.SetActive(true);
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}