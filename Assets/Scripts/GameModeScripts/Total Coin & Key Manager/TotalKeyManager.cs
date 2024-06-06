using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalKeyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text totalKeysText;

    private void Start()
    {
        UpdateKeyCount();
    }

    public void UpdateKeyCount()
    {
        int totalKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        totalKeysText.text = " " + totalKeys.ToString();
    }

    public void AddKeys(int amount)
    {
        int totalKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        totalKeys += amount;
        PlayerPrefs.SetInt("TotalKeys", totalKeys);
        UpdateKeyCount();
    }

    public void SpendKeys(int amount)
    {
        int totalKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        totalKeys = Mathf.Max(0, totalKeys - amount);
        PlayerPrefs.SetInt("TotalKeys", totalKeys);
        UpdateKeyCount();
    }
}
