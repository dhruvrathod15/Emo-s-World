using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    /*[SerializeField] private ScoreCounter scoreCounter; // Reference to the ScoreCounter script*/
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
            // Enemy defeated: Increase score and deactivate the GameObject
          /*  scoreCounter.IncrementScore(5);*/
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Check and apply damage to Health component if it exists
            Health healthComponent = collider.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }

            // Check and apply damage to PlayerHealthLevels component if it exists
            PlayerHealthLevels playerHealthLevelsComponent = collider.GetComponent<PlayerHealthLevels>();
            if (playerHealthLevelsComponent != null)
            {
                playerHealthLevelsComponent.TakeDamage(damage);
            }
        }
    }
}
