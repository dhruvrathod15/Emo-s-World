using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField] private float gameDuration = 30f; 
    [SerializeField] private Image timerImage; 
    [SerializeField] private GameObject pnlTimeOver;
    [SerializeField] GameObject pnlGameUI;
    private float gameEndTime = 0f;
    private bool isGameOver = false;
    private void Start()
    {
        if (timerImage != null)
        {
            timerImage.type = Image.Type.Filled;
            timerImage.fillAmount = 1f; // Full at the start
        }
    }
    private void Update()
    {
        if (!isGameOver)
        {
            gameEndTime += Time.deltaTime;
            float remainingTime = gameDuration - gameEndTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                FindObjectOfType<SuperManagerTimer>().GamePlay.Stop();
                GameOver();
            }
            UpdateTimerImage(remainingTime);
        }
    }
    private void UpdateTimerImage(float remainingTime)
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = remainingTime / gameDuration;
        }
    }
    private void GameOver()
    {
        isGameOver = true;
        pnlTimeOver.SetActive(true);
        pnlGameUI.SetActive(true);
    }
    public void AddTime(float additionalTime)
    {
        gameDuration += additionalTime;
        UpdateTimerImage(gameDuration - gameEndTime); //Update the 5 sec time duration from player hits the timer tag.
    }
}
