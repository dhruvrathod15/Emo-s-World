using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float demage;
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
                animator.SetTrigger("idle");
                Vector3 scale = transform.localScale;
                scale.x = 3.5f;
                transform.localScale = scale;
                transform.localPosition = new Vector3(transform.localPosition.x - speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                animator.SetTrigger("idle");
                Vector3 scale = transform.localScale;
                scale.x = -3.5f;
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
            animator.SetTrigger("isInjured");
            gameObject.SetActive(false);
        }
    }
    public void DestroyEnemy()
    {
            animator.SetTrigger("isInjured");
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(demage);
        }
    }
}