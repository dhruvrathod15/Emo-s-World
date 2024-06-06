using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDailyRewards : MonoBehaviour
{
    InputSystem controls;
    public float moveSpeed = 6f;
    public float boostedJumpForce = 15f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    public SuperManagerDailyRewards superManager;
    private float direction = 0f;
    public GroundPooling Pool;
    public GameObject player;
    public int poolSize = 10;
    public float nextXPosition = 85.98f;
    private GameObject currentGround;
    [SerializeField] CoinManagerDailyRewards coinManager;
    [SerializeField] KeyManagerDailyRewards keyManager;
    [SerializeField] GameObject pnlGameOver;
    [SerializeField] GameObject pnlGameUI;

    private void Awake()
    {
        superManager.GamePlay.Play();
        Application.targetFrameRate = 90;
        FindObjectOfType<CoinManagerDailyRewards>().ResetCoins();
        FindObjectOfType<KeyManagerDailyRewards>().ResetKey();
        controls = new InputSystem();
        controls.Enable();
        controls.MobileMovement.Movement.performed += ctx => direction = ctx.ReadValue<float>();
        controls.MobileMovement.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextXPosition = 85.98f;
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

    private void Jump()
    {
        if (isGrounded)
        {
            superManager.PlayerJump.Play();
            rb.velocity = new Vector2(rb.velocity.x, boostedJumpForce);
            animator.SetTrigger("isJumping");
        }
    }

    private void FlipSprite()
    {
        if (direction < -0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -0.7f;
            transform.localScale = scale;
        }
        else if (direction > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0.7f;
            transform.localScale = scale;
        }
    }

    public bool canAttack()
    {
        return true;
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
        if (collision.gameObject.CompareTag("DestroyPoint"))
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("End"))
        {
            player.SetActive(false);
            pnlGameUI.SetActive(false);
            pnlGameOver.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Finish"))
        {
            GameObject ground = Pool.GetObjectFromPool();
            ground.transform.position = new Vector2(nextXPosition, 0f);
            nextXPosition += 85.98f;
            currentGround = ground;
            collider.gameObject.SetActive(false);
        }
        else if (collider.gameObject.CompareTag("Coin"))
        {
            superManager.CoinCollectible.Play();
            collider.gameObject.SetActive(false);
            coinManager.CollectCoin();
        }
        else if (collider.gameObject.CompareTag("Key"))
        {
            superManager.CoinCollectible.Play();
            collider.gameObject.SetActive(false);
            keyManager.CollectKey();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void ResetCoins()
    {
        FindObjectOfType<TimerCoinManager>().ResetCoins();
    }
}