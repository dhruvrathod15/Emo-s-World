using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerControllerForLevels : MonoBehaviour
{
    InputSystem controls;
    public float moveSpeed = 6f; // Adjust the speed as needed
    public float jumpForce = 15f; // Adjust the jump force as needed
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private float direction;
    [SerializeField] GameObject Player;
    [SerializeField] SuperManagerLevels superManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;
    [SerializeField] CoinManager coinManager;
    [SerializeField] GameObject pnlLevelCompleted;
    private void Awake()
    {
        Application.targetFrameRate = 90;
        controls = new InputSystem();
        controls.Enable();
        controls.MobileMovement.Movement.performed += ctx => direction = ctx.ReadValue<float>();
        controls.MobileMovement.Movement.canceled += ctx => StopMovement();
        controls.MobileMovement.Jump.performed += ctx => Jump();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FindObjectOfType<CoinManager>().ResetCoins();
        FindObjectOfType<KeyManagerLevels>().ResetKey();
    }
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        Vector2 movement = new Vector2(direction, 0f) * moveSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
        animator.SetBool("isRunning", Mathf.Abs(direction) > 0.1f);
        FlipSprite();
    }
    public bool canAttack()
    {

        return true;
    }
    private void Jump()
    {
        if (isGrounded)
        {
            superManager.PlayerJump.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("isJumping");
        }
    }
    private void FlipSprite()
    {
        if (direction < -0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -0.75f;
            transform.localScale = scale;
        }
        else if (direction > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0.75f;
            transform.localScale = scale;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Gift"))
        {
            Debug.Log("Gift Collected");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DestroyPoint"))
        {
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("VenomSea") || collision.gameObject.CompareTag("End") || collision.gameObject.CompareTag("Water"))
        {
            superManager.PlayerDeath.Play();
            superManager.GamePlay.Stop();
            pnlGameOver.SetActive(true);
            pnlGameUI.SetActive(false);
            Time.timeScale = 0.0f;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            superManager.GamePlay.Stop();
            animator.SetTrigger("IsIdle");
           /* Player.SetActive(false);*/
            Time.timeScale = 1.0f;
            pnlLevelCompleted.SetActive(true);
            pnlGameUI.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            coinManager.CollectCoin();
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            superManager.CoinCollectible.Play();
            collision.gameObject.SetActive(false);
            FindObjectOfType<KeyManagerLevels>().CollectKey();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void StopMovement()
    {
        direction = 0;
        RestPosition();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    public void RestPosition()
    {
        Debug.Log("RestPosition called");
        direction = 0; // Reset the direction to 0
        rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
        if (animator != null)
        {
            animator.SetTrigger("IsIdle");
            Debug.Log("IsIdle trigger set");
        }
    }
    public void ResetHealth()
    {
        FindObjectOfType<PlayerHealthLevels>().ResetHealth();
    }
    public void ResetCoins()
    {
        FindObjectOfType<CoinManager>().ResetCoins();
    }
}
