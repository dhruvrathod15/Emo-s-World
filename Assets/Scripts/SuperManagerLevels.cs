using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManagerLevels : SerializedMonoBehaviour
{
    [Header("UI Dictionary")]
    public Dictionary<string, GameObject> levelUIElementDictionary;
    [Header("Level Selection Keys")]
    public string unlockedLevelsKey = "UnlockButtons";
    public string currentLevelIndexKey = "CompletedLevelIndex";
    [Header("Keys")]
    public string TotalCoinsGameMode = "TotalCoinsGameMode";
    public string Coins = "CountCoins";
    public string Keys = "CountKeys";
    public string TotalKeys = "TotalKeys";
    public string TotalCoins = "TotalCoins";
    [Header("Game Audios")]
    public AudioSource GamePlay;
    public AudioSource PlayerJump;
    public AudioSource AppleAttack;
    public AudioSource CoinCollectible;
    public AudioSource PlayerDeath;
    public AudioSource PlayerHurt;
    public AudioSource HealthCollectible;
    public AudioSource ButtonClicked;
}

