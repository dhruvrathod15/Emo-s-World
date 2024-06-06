using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DailyRewardsScripts : MonoBehaviour
{
    [SerializeField] SuperManagerDailyRewards superManager;

    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary,"btnBack").onClick.AddListener(Back);
    }
    void Back()
    {
        SceneManager.LoadScene("GameModeScene");
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}
