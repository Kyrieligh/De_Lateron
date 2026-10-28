using System.Collections.Generic;
using UnityEngine;

public class MinotourBoss : Enemy
{
    private List<PathNode> currentPath;
    private int currentPathIndex;

    private float pathUpdateTimer = 0.5f; // Hitung ulang rute setiap 0.5 detik agar tidak lag

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

    private void Update()
    {
        pathUpdateTimer -= Time.deltaTime;
        if (pathUpdateTimer <= 0f)
        {
            pathUpdateTimer = 0.5f;
            RequestPathToPlayer();
        }

        HandleMovement();
    }

    private void RequestPathToPlayer()
    {
        if (Player.Instance == null) return;

        Vector3 playerPos = Player.Instance.transform.position;
        Vector3 myPos = transform.position;

        // Konversi koordinat dunia ke indeks grid integer
        int startX = Mathf.FloorToInt(myPos.x);
        int startY = Mathf.FloorToInt(myPos.y);
        int targetX = Mathf.FloorToInt(playerPos.x);
        int targetY = Mathf.FloorToInt(playerPos.y);

        currentPath = Pathfinding.Instance.FindPath(startX, startY, targetX, targetY);
        currentPathIndex = 0;
    }

    private void HandleMovement()
    {
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
            return;

        // Ambil titik tengah dari tile tujuan
        Vector3 targetPosition = new Vector3(currentPath[currentPathIndex].x + 0.5f, currentPath[currentPathIndex].y + 0.5f, 0);

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPathIndex++; // Lanjut ke node berikutnya dalam rute
        }
    }
}
