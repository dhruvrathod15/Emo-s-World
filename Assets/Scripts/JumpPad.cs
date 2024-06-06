using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float JumpForce = 26f;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("isJump");
            collision.gameObject.GetComponent<Animator>().SetTrigger("isJumping");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);
        }
    }
}
