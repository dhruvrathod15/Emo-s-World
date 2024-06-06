using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] HomeSuperManager superManager;
    private void Awake()
    {
        GetButtonComponentFromDictionary(superManager.HomeScreenUIDictionary, "btnstart").onClick.AddListener(startGame);
    }
    private void startGame()
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
