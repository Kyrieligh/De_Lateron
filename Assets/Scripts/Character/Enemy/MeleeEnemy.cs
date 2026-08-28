using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float nextDamageTime;

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
                if (player != null)
                {
                    player.TakeDamage(AttackDamage);
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}
