using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManagerDailyRewards : SerializedMonoBehaviour
{
    public Dictionary<string, GameObject> RewardsUIElementDictionary;
    [Header("Coins Key")]
    public string Coins = "CountCoins";
    public string Keys = "CountKeys";
    public string TotalKeys = "TotalKeys";
    public string TotalCoins = "TotalCoins";
    public AudioSource GamePlay;
    public AudioSource PlayerJump;
    public AudioSource AppleAttack;
    public AudioSource CoinCollectible;
    public AudioSource PlayerDeath;
    public AudioSource PlayerHurt;
    public AudioSource HealthCollectible;
    public AudioSource ButtonClicked;
}
