using TMPro;
using UnityEngine;
public class TimerCoinManager : MonoBehaviour
{
    [SerializeField] SuperManagerTimer superManager;
    public TMP_Text coinText;
    private int coinCount = 0;

    private void Start()
    {
        coinCount = PlayerPrefs.GetInt(superManager.Coins, 0);
        UpdateCoinText();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
    public void CollectCoin()
    {
        coinCount++;
        superManager.CoinCollectible.Play();
        PlayerPrefs.SetInt(superManager.Coins, coinCount);
        // Increment total coins and save
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins++;
        PlayerPrefs.SetInt(superManager.TotalCoins, totalCoins);

        PlayerPrefs.Save();
        UpdateCoinText();
    }


    public void ResetCoins()
    {
        coinCount = 0;
        PlayerPrefs.SetInt(superManager.Coins, coinCount);
        PlayerPrefs.Save();
        Debug.Log("Coins have been reset.");
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "X " + coinCount.ToString();
    }
}