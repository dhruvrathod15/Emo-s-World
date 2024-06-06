using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthLevels : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] SuperManagerLevels superManagerLevels;
    [SerializeField] private float startingHealth;
    [SerializeField] GameObject Player;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;
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
            superManagerLevels.PlayerHurt.Play();
            anim.SetTrigger("Dead");
            PlayBloodParticle();
        }
        else
        {
            superManagerLevels.PlayerDeath.Play();
            superManagerLevels.GamePlay.Stop();
            Time.timeScale = 0.0f;
            pnlGameOver.SetActive(true);
            pnlGameUI.SetActive(true);
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