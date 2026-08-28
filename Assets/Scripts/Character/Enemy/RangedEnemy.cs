using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack Settings")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float shootCoolDown = 1.3f;
    //[SerializeField] private Transform playerTransform;
    //[SerializeField] private float rotationSpeed = 3.7f;
    private float nextShootTime;

    public void Update()
    {
        if (target == null) return;
        AimAtPlayer();
        
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            Vector3 moveToTarget = (target.position - transform.position).normalized;
            moveDirection = moveToTarget;
        }
        else
        {
            // Jika sudah dalam jangkauan, berhenti dan mulai menembak
            moveDirection = Vector2.zero;

            if (Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootCoolDown;
            }
        }
    }


    private void AimAtPlayer()
    {
        Vector3 currentScale = transform.localScale;
        if (target.position.x < transform.position.x)
        {
            // Player di sebelah kiri
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else
        {
            // opposite from above
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;

        // firePoint to player
        if (firePoint != null)
        {
            Vector2 direction = target.position - firePoint.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Putar firePoint pada sumbu Z
            firePoint.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    private void Shoot()
    {
        if (Bullet == null || firePoint == null) return;
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }

        Destroy(bullet, 2.1f);
    }
}
