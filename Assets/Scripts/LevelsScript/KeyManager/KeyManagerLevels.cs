using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManagerLevels : MonoBehaviour
{
    [SerializeField] SuperManagerLevels superManager;
    public TMP_Text KeyText;
    private int keyCount = 0;

    private void Start()
    {
        keyCount = PlayerPrefs.GetInt(superManager.Keys, 0);
        UpdateCoinText();
    }

    public int GetCoinCount()
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
        UpdateCoinText();
    }


    public void ResetKey()
    {
        keyCount = 0;
        PlayerPrefs.SetInt(superManager.Keys, keyCount);
        PlayerPrefs.Save();
        Debug.Log("Coins have been reset.");
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        KeyText.text = "X " + keyCount.ToString();
    }
}
