using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StatCharacter, IDamageable
{
    //  variabel untuk mengatur kecepatan damage
    public float damageInterval = 1.0f; // Damage masuk setiap 1 detik
    
    
    //[System.Serializable] public enum Enemylist { RangeEnemy, MeleeEnemy };
    //public Enemylist type;


    //public Player player;
    [Header("Targeting")]
    [SerializeField] protected Transform target;// varible target use for enemy for find player

    [Header("Poin Settings")] 
    public int scoreValue;

    //Drop item
    [Header("Drop Item")]
    public List<PickUpItem> dropTable = new List<PickUpItem>();

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
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.UpdatePoint(scoreValue);
            }
            Die();
        }
    }

    void Die()
    {
        foreach(PickUpItem item in dropTable)
        {
            //chance to get item
            float chance = Random.Range(0f, 100f); 
            if ( chance <= item.dropChance)
            {

                InstantiateItem(item.itemPrefab);
            }
            //break;
        }

        Destroy(gameObject);
    }

    void InstantiateItem(GameObject loot)
    {
        if(loot)
        {
            //GameObject droppedItem = 
              Instantiate(loot, transform.position, Quaternion.identity);
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
