using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float nextDamageTime;

    protected virtual void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Cek apakah yang ditabrak adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // interval serangan
            if (Time.time >= nextDamageTime)
            {

                IDamageable player = collision.gameObject.GetComponent<IDamageable>();
                if (player != null)
                {
                    player.TakeDamage(AttackDamage);
                    nextDamageTime = Time.time + damageInterval;
                    Debug.Log("MeleeEnemy menyerang Player dengan damage: " + AttackDamage);
                }
            }
        }
    }
}
