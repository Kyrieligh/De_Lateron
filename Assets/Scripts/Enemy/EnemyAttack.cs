using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerHealth player;
    public int damage = 5;

    // Tambahkan variabel untuk mengatur kecepatan damage
    public float damageInterval = 1.0f; // Damage masuk setiap 1 detik
    private float nextDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Cek apakah yang ditabrak adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cek apakah sudah waktunya memberikan damage lagi
            if (Time.time >= nextDamageTime)
            {
                // Jika variabel 'player' belum diisi di Inspector, coba ambil otomatis
                if (player == null)
                {
                    player = collision.gameObject.GetComponent<PlayerHealth>();
                }

                if (player != null)
                {
                    player.TakeDamage(damage);
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}