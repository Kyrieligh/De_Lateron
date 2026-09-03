using UnityEngine;

public class PlayerBullet : Bullet
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return; // ignore collision with tag "Player"

        Debug.Log("Bullet Collision : " + collision.gameObject.name);
        IDamageable enemyDamageable = collision.GetComponent<IDamageable>();
        if (enemyDamageable != null || collision.CompareTag("Enemy"))
        {
            enemyDamageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
