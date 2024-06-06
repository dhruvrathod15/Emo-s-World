using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSawEnemy : MonoBehaviour
{ 
    [SerializeField] private float damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerTimerHealth>().TakeDamage(damage);
            Debug.Log($"Player hit! Damage dealt: {damage}");
        }
    }
}
