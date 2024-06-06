using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManager : SerializedMonoBehaviour
{
    public Dictionary<string, GameObject> UIElementDictionary;
    public AudioSource GamePlay;
    public AudioSource PlayerJump;
    public AudioSource AppleAttack;
    public AudioSource CoinCollectible;
    public AudioSource PlayerDeath;
    public AudioSource PlayerHurt;
    public AudioSource HealthCollectible;
    public AudioSource ButtonClicked;
}
