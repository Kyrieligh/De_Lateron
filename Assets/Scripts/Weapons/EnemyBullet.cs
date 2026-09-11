using UnityEngine;

public class EnemyBullet : Bullet
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        // Hancurkan otomatis setelah durasi tertentu jika tidak mengenai apapun
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Bergerak maju ke arah depan lokal objek setiap frame
        // Ganti Vector3.right dengan Vector3.up jika sprite peluru menghadap ke atas
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet Collision : " + collision.gameObject.name);

        // 1. Abaikan jika menyentuh musuh / penembak itu sendiri
        if (collision.gameObject.CompareTag("Enemy")) return;

        // 2. Hanya proses jika menyentuh objek dengan tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable playerDamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            if (playerDamageable != null)
            {
                playerDamageable.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        // 3. Hancur jika menabrak rintangan atau tembok
        //    else if (collision.gameObject.CompareTag("Obstacle"))
        //    {
        //        Destroy(gameObject);
        //    }
    }
}