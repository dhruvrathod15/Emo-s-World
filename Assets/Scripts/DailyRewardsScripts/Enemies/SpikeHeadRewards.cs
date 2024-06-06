using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadRewards : MonoBehaviour
{
    [SerializeField] private float demage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthRewards>().TakeDamage(demage);
        }
    }
}