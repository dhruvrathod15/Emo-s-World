using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] GameObject Player;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] SuperManagerTimer superManagerTimer;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] GameObject pnlGameOver;
    public float currentHealth { get; private set; }
    private Animator anim;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            superManagerTimer.PlayerHurt.Play();
            anim.SetTrigger("Dead");
            PlayBloodParticle();
        }
        else
        {
            Time.timeScale = 0.0f;
            superManagerTimer.PlayerDeath.Play();
            superManagerTimer.GamePlay.Stop();
            pnlGameOver.SetActive(true);
            pnlGameUI.SetActive(false);
            Player.SetActive(false);
        }
    }
    private void PlayBloodParticle()
    {
        if (bloodParticle != null)
        {
            bloodParticle.transform.position = Player.transform.position;
            bloodParticle.Play();
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void ResetHealth()
    {
        currentHealth = startingHealth;
    }
}
