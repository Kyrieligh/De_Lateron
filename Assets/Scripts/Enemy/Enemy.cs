using UnityEngine;

public class Enemy : Character, IDamageable
{
    //  variabel untuk mengatur kecepatan damage
    public float damageInterval = 1.0f; // Damage masuk setiap 1 detik
    private float nextDamageTime;

    //[System.Serializable] public enum Enemylist { RangeEnemy, MeleeEnemy };
    //public Enemylist type;

    //public Player player;
    [Header("Targeting")]
    [SerializeField] protected Transform target;// varible target use for enemy for find player

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


    private void OnCollisionStay2D(Collision2D collision)
    {
        // Cek apakah yang ditabrak adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cek apakah sudah waktunya memberikan damage lagi
            if (Time.time >= nextDamageTime)
            {
                
                IDamageable player = collision.gameObject.GetComponent<IDamageable>();
                // Jika variabel 'player' belum diisi di Inspector, coba ambil 
                if (player!= null)
                {
                    player.TakeDamage(AttackDamage);
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }

}
