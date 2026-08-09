using UnityEngine;

public class Enemy : Character
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    //public int currentHealth, maxHealth = 20;
    [Header("Score Settings")] public int scoreValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            //if (ScoreManager.instance !=null)
            //{
            //    ScoreManager.instance.UpdateScore(scoreValue);
            //}
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
