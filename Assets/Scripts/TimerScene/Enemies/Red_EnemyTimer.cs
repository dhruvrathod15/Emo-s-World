using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_EnemyTimer : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Animator animator;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    public int maxHealth = 2;
    private int currentHealth;

    private void Awake()
    {
        leftEdge = transform.localPosition.x - movementDistance;
        rightEdge = transform.localPosition.x + movementDistance;
        currentHealth = maxHealth; // Initialize currentHealth to maxHealth
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.localPosition.x > leftEdge)
            {
                animator.SetTrigger("isRunning");
                Vector3 scale = transform.localScale;
                scale.x = -0.75f;
                transform.localScale = scale;
                transform.localPosition = new Vector3(transform.localPosition.x - speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.localPosition.x < rightEdge)
            {
                animator.SetTrigger("isRunning");
                Vector3 scale = transform.localScale;
                scale.x = 0.75f;
                transform.localScale = scale;
                transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
                movingLeft = true;
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

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