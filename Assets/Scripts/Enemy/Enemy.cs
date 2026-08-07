using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public int health, maxHealth = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
}
