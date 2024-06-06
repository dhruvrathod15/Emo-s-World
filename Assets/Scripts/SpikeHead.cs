using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float demage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerTimerHealth>().TakeDamage(demage);
        }
    }
}