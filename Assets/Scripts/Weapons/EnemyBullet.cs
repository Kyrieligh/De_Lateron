using UnityEngine;

public class EnemyBullet : Bullet
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable playerDamageable = collision.GetComponent<IDamageable>();
        if (playerDamageable != null || collision.CompareTag("Player"))
        {
            playerDamageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
