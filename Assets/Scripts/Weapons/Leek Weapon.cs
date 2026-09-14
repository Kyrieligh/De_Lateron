using UnityEngine;

public class LeekWeapon : MonoBehaviour
{
    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int meleeDamage = 7;
    [SerializeField] public float attackRadius = 0.3f;
    [SerializeField] private Animator animator;
    

    [Header("Cooldown")]
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (animator == null) animator =  GetComponent<Animator>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            MeleeAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void MeleeAttack()
    {
        //if (animator != null)
        //{
        //    animator.SetTrigger("triggerAttack");
        //}

        animator.SetTrigger("triggerAttack");

        Transform point = attackPoint != null ? attackPoint : transform;
        // Specify a radius for the OverlapCircleAll call (e.g., 0.5f)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (enemyCollider.gameObject.CompareTag("Player")) return;

            IDamageable enemy = enemyCollider.gameObject.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(meleeDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Transform point = attackPoint != null ? attackPoint : transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point.position, attackRadius);
    }
}
