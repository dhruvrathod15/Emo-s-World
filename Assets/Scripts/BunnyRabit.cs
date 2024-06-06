using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyRabit : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float demage;
    /* [SerializeField] ScoreCounter scoreCounter;*/
    private Animator animator;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    public int maxHealth = 1; // Set the maximum health (number of hits allowed)1
    private int currentHealth;

    private void Awake()
    {
        leftEdge = transform.localPosition.x - movementDistance;
        rightEdge = transform.localPosition.x + movementDistance;
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
                Vector3 scale = transform.localScale;
                scale.x = 3f;
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
                Vector3 scale = transform.localScale;
                scale.x = -3f;
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

            animator.SetTrigger("isDead");
            // Enemy defeated: Deactivate the GameObject
            gameObject.SetActive(false);
        }
    }
    public void DestroyEnemy()
    {
        /*scoreCounter.IncrementScore(5);*/
        animator.SetTrigger("isDead");
        // Enemy defeated: Deactivate the GameObject
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(demage);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthLevels>().TakeDamage(demage);
        }
    }
}
