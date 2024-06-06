using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    /* [SerializeField] ScoreCounter scoreCounter;*/
    private Animator animator;
    private bool movingLeft = true; // Initialize to true or false depending on the desired start direction
    private float leftEdge;
    private float rightEdge;
    public int maxHealth = 1; // Set the maximum health (number of hits allowed)1
    private int currentHealth;

    private void Awake()
    {
        leftEdge = transform.localPosition.x - movementDistance;
        rightEdge = transform.localPosition.x + movementDistance;
        Debug.Log($"Rock initialized: leftEdge = {leftEdge}, rightEdge = {rightEdge}");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.localPosition.x > leftEdge)
            {
                /* animator.SetTrigger("idle");*/
                Vector3 scale = transform.localScale;
                scale.x = 2.5f;
                transform.localScale = scale;
                transform.localPosition = new Vector3(transform.localPosition.x - speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
            {
                movingLeft = false;
                Debug.Log("Reached left edge, changing direction to right.");
            }
        }
        else
        {
            if (transform.localPosition.x < rightEdge)
            {
                /*animator.SetTrigger("idle");*/
                Vector3 scale = transform.localScale;
                scale.x = -2.5f;
                transform.localScale = scale;
                transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
            {
                movingLeft = true;
                Debug.Log("Reached right edge, changing direction to left.");
            }
        }
        Debug.Log($"Rock position: {transform.localPosition}");
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            // Enemy defeated: Deactivate the GameObject
            gameObject.SetActive(false);
        }
    }

    public void DestroyEnemy()
    {
        // Enemy defeated: Deactivate the GameObject
        gameObject.SetActive(false);
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
