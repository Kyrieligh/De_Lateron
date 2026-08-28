using UnityEngine;

public class PlayerBullet : Bullet
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable enemyDamageable = collision.GetComponent<IDamageable>();
        if (enemyDamageable != null || collision.CompareTag("Enemy"))
        {
            enemyDamageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
