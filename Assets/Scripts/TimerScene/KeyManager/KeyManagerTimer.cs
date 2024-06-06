using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManagerTimer : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    public TMP_Text KeyText;
    private int keyCount = 0;

    private void Start()
    {
        keyCount = PlayerPrefs.GetInt(superManager.Keys, 0);
        UpdateKeyText();
    }

    public int GetKeyCount()
    {
        return keyCount;
    }
    public void CollectKey()
    {
        keyCount++;
        superManager.CoinCollectible.Play();
        PlayerPrefs.SetInt(superManager.Keys, keyCount);
        // Increment total coins and save
        int totalCoins = PlayerPrefs.GetInt("TotalKeys", 0);
        totalCoins++;
        PlayerPrefs.SetInt(superManager.TotalKeys, totalCoins);

        PlayerPrefs.Save();
        UpdateKeyText();
    }


    public void ResetKey()
    {
        keyCount = 0;
        PlayerPrefs.SetInt(superManager.Keys, keyCount);
        PlayerPrefs.Save();
        Debug.Log("Coins have been reset.");
        UpdateKeyText();
    }

    private void UpdateKeyText()
    {
        KeyText.text = "X " + keyCount.ToString();
    }
}