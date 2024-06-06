using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<SuperManagerTimer>().HealthCollectible.Play();
            animator.SetTrigger("isCollected");
            collision.GetComponent<PlayerTimerHealth>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}

