using TMPro;
using UnityEngine;

public class TotalCoinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCoinText;

    private void Start()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinText.text = " " + totalCoins.ToString();
    }
}
