using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSpikeHead : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerTimerHealth playerTimerHealth = collision.GetComponent<PlayerTimerHealth>();
            PlayerHealthRewards playerHealthRewards = collision.GetComponent<PlayerHealthRewards>();

            if (playerTimerHealth != null)
            {
                playerTimerHealth.TakeDamage(damage);
            }
            if (playerHealthRewards != null)
            {
                playerHealthRewards.TakeDamage(damage);
            }
        }
    }
}
