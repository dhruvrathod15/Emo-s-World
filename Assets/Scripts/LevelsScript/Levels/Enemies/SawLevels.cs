using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawLevels : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.localPosition.x - movementDistance;
        rightEdge = transform.localPosition.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.localPosition.x > leftEdge)
            {
                transform.localPosition = new Vector3(transform.localPosition.x - speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.localPosition.x < rightEdge)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else
                movingLeft = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealthLevels>().TakeDamage(damage);
        }
    }
}
