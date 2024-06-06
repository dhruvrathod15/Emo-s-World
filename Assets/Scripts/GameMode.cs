using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class GameMode : MonoBehaviour
{
    [SerializeField] SuperManagerGameMode superManager;
    [SerializeField] GameObject imgLock;
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtTitle;
    [SerializeField] GameObject Key;
    [SerializeField] TotalKeyManager totalKeyManager;
    [SerializeField] GameObject pnlShop;
    [SerializeField] GameObject CoinWarnning;
    [SerializeField] GameObject KeyWarnning;
    [SerializeField] GameObject RewardWarnning;
    [SerializeField] GameObject LockWarnning;
    [SerializeField] ParticleSystem BuyParticleSystem;

    private void Awake()
    {
        txtTitle.gameObject.SetActive(true);
        imgLock.SetActive(false);
        txtTimer.gameObject.SetActive(false);
        Key.SetActive(false);
        RewardWarnning.SetActive(false);
        LockWarnning.SetActive(false);
        if (!IsDailyRewardAvailable())
        {
            txtTitle.gameObject.SetActive(false);
            imgLock.SetActive(true);
            txtTimer.gameObject.SetActive(true);
            Key.SetActive(true);
            RewardWarnning.SetActive(false);
            LockWarnning.SetActive(false);
            StartCoroutine(StartCountdown());
        }

        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnendless").onClick.AddListener(endlessGameMode);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnlevels").onClick.AddListener(levelsGameMode);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btntimer").onClick.AddListener(timerGameMode);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnfight").onClick.AddListener(DailyRewardsMode);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnKey").onClick.AddListener(BuyDailyRewards);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnBuyCoins").onClick.AddListener(BuyCoins);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnBuyKeys").onClick.AddListener(BuyKeys);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnBack").onClick.AddListener(Back);
        GetButtonComponentFromDictionary(superManager.GameModeUIDictionary, "btnShop").onClick.AddListener(Shop);
    }

    private void endlessGameMode()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("EndlessScene");
    }

    private void levelsGameMode()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("LevelsScene");
    }

    private void timerGameMode()
    {
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("TimerScene");
    }

    private void DailyRewardsMode()
    {
        if (IsDailyRewardAvailable())
        {
            superManager.ButtonClicked.Play();
            SceneManager.LoadScene("DailyRewardsScene");
            PlayerPrefs.SetString("DailyRewardLastClaimed", DateTime.Now.ToString());

            txtTitle.gameObject.SetActive(false);
            imgLock.SetActive(true);
            txtTimer.gameObject.SetActive(true);
            Key.SetActive(true);
            RewardWarnning.SetActive(false);
            StartCoroutine(StartCountdown());
        }
        else
        {
            LockWarnning.SetActive(true);
            txtTitle.gameObject.SetActive(false);
            imgLock.SetActive(false);
            txtTimer.gameObject.SetActive(false);
            Key.SetActive(false);
            StartCoroutine(DisableWarningAfterDelay(LockWarnning, 5));
            Debug.Log("Daily Rewards mode is locked. Please try after 24 Hours.");
            // Optionally, show a message to the player.
        }
    }

    private bool IsDailyRewardAvailable()
    {
        if (!PlayerPrefs.HasKey("DailyRewardLastClaimed"))
        {
            return true;
        }

        DateTime lastClaimed = DateTime.Parse(PlayerPrefs.GetString("DailyRewardLastClaimed"));
        TimeSpan timeSinceLastClaimed = DateTime.Now - lastClaimed;

        return timeSinceLastClaimed.TotalHours >= 24;
    }

    private IEnumerator StartCountdown()
    {
        DateTime lastClaimed = DateTime.Parse(PlayerPrefs.GetString("DailyRewardLastClaimed"));
        DateTime targetTime = lastClaimed.AddHours(24);
        while (DateTime.Now < targetTime)
        {
            TimeSpan remainingTime = targetTime - DateTime.Now;
            txtTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
            yield return new WaitForSeconds(1);
        }

        txtTitle.gameObject.SetActive(true);
        imgLock.SetActive(false);
        txtTimer.gameObject.SetActive(false);
        Key.SetActive(false);
        RewardWarnning.SetActive(false);
    }

    private void BuyDailyRewards()
    {
        int totalKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        if (totalKeys >= 5)
        {
            totalKeyManager.SpendKeys(5);
            PlayerPrefs.SetString("DailyRewardLastClaimed", DateTime.Now.ToString());
            txtTitle.gameObject.SetActive(true);
            imgLock.SetActive(false);
            txtTimer.gameObject.SetActive(false);
            Key.SetActive(false);

            // Unlock daily rewards mode for immediate access
            StartCoroutine(UnlockDailyRewardsTemporarily());
        }
        else
        {
            Debug.Log("Not enough keys to buy daily rewards.");
            RewardWarnning.SetActive(true);
            txtTitle.gameObject.SetActive(false);
            imgLock.SetActive(false);
            txtTimer.gameObject.SetActive(false);
            Key.SetActive(false);
            StartCoroutine(DisableWarningAfterDelay(RewardWarnning, 5));
            // Optionally, show a message to the player.
        }
    }

    private IEnumerator UnlockDailyRewardsTemporarily()
    {
        // Allow immediate access to DailyRewardsScene once
        superManager.ButtonClicked.Play();
        SceneManager.LoadScene("DailyRewardsScene");

        // Wait until scene load is completed before updating the UI
        yield return new WaitForSeconds(0.5f);

        // Start the countdown for the next available daily reward
        txtTitle.gameObject.SetActive(false);
        imgLock.SetActive(true);
        txtTimer.gameObject.SetActive(true);
        Key.SetActive(true);
        RewardWarnning.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    void BuyCoins()
    {
        superManager.ButtonClicked.Play();
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (totalCoins >= 500)
        {
            totalCoins -= 500;
            totalCoins += 250;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            FindObjectOfType<TotalCoinManager>().UpdateCoinCount();
            BuyParticleSystem.gameObject.SetActive(true);
            BuyParticleSystem.Play();
            Debug.Log("Coins bought successfully!");
        }
        else
        {
            CoinWarnning.SetActive(true);
            StartCoroutine(DisableWarningAfterDelay(CoinWarnning, 5));
            Debug.Log("Not enough coins to buy a coin.");
        }
    }

    void BuyKeys()
    {
        superManager.ButtonClicked.Play();
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int totalKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        if (totalCoins >= 500)
        {
            totalCoins -= 500;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            totalKeys += 10;
            PlayerPrefs.SetInt("TotalKeys", totalKeys);
            FindObjectOfType<TotalCoinManager>().UpdateCoinCount();
            FindObjectOfType<TotalKeyManager>().UpdateKeyCount();
            BuyParticleSystem.gameObject.SetActive(true);
            BuyParticleSystem.Play();
            Debug.Log("Keys bought successfully!");
        }
        else
        {
            KeyWarnning.SetActive(true);
            StartCoroutine(DisableWarningAfterDelay(KeyWarnning, 5));
            Debug.Log("Not enough coins to buy keys.");
        }
    }

    void Back()
    {
        superManager.ButtonClicked.Play();
        pnlShop.SetActive(false);
    }

    void Shop()
    {
        superManager.ButtonClicked.Play();
        pnlShop.SetActive(true);
    }

    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }

    private IEnumerator DisableWarningAfterDelay(GameObject warningObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        txtTitle.gameObject.SetActive(false);
        imgLock.SetActive(true);
        txtTimer.gameObject.SetActive(true);
        Key.SetActive(true);
        RewardWarnning.SetActive(false);
        warningObject.SetActive(false);
    }
}