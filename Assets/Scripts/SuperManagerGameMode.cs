using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManagerGameMode : SerializedMonoBehaviour
{
    public Dictionary<string, GameObject> GameModeUIDictionary;
    public AudioSource ButtonClicked;
    public string TotalKeys = "TotalKeys";
    public string TotalCoins= "TotalCoinsGameMode";
}