using UnityEngine;

public class LeekWeapon : MonoBehaviour
{
    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int meleeDamage = 7;
    [SerializeField] public float attackRadius = 0.3f;
    [SerializeField] private Animator animator;
    private Vector2 pointerInput;

    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pointerInput = GetPointerInput();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            MeleeAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void MeleeAttack()
    {
        animator.SetTrigger("triggerAttack");
        // Specify a radius for the OverlapCircleAll call (e.g., 0.5f)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        foreach (Collider2D Collider in hitEnemies)
        {
            if (Collider.gameObject.CompareTag("Player")) return;

            IDamageable enemy = Collider.gameObject.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(meleeDamage);
            }
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToViewportPoint(mousePos);
    }
}
