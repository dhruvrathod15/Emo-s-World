using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;

        if (collision.tag == "Player" || collision.tag == "Enemy")
            collision.GetComponent<Health>()?.TakeDamage(1);

        if (anim != null) { 
            anim.SetTrigger("explode"); 
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Assuming the enemy has a "Enemy" tag
            var TurtleEnemy = collision.gameObject.GetComponent<TurtleEnemy>();
            var BunnyRabit = collision.gameObject.GetComponent<BunnyRabit>();
            var RedEnemy = collision.gameObject.GetComponent<Enemy>();
            if (TurtleEnemy != null)
            {

                TurtleEnemy.TakeDamage();
            }
            if (RedEnemy != null)
            {
                RedEnemy.TakeDamage();
            }
            if (BunnyRabit != null)
            {
                BunnyRabit.TakeDamage();
            }
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}