using System.Collections.Generic;
using UnityEngine;

public class MinotourBoss : Enemy
{
    [Header("A* Pathfinding")]
    public GridPathfinding pathfinding;

    private List<Vector2> currentPath;
    private int currentPathIndex;
    private float pathUpdateTimer;
    private float nextDamageTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

        if (pathfinding == null)
        {
            pathfinding = FindAnyObjectByType<GridPathfinding>();
        }

        RequestPathToPlayer();
    }

    private void Update()
    {
        if (target == null) return;

        pathUpdateTimer -= Time.deltaTime;
        if (pathUpdateTimer <= 0f)
        {
            pathUpdateTimer = 0.25f; // Update jalur lebih responsif saat player menghindar
            RequestPathToPlayer();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void RequestPathToPlayer()
    {
        if (target == null || pathfinding == null) return;

        Vector2 playerPos = target.position;
        Vector2 myPos = transform.position;

        List<Vector2> newPath = pathfinding.FindPathWorld(myPos, playerPos);
        if (newPath != null && newPath.Count > 0)
        {
            currentPath = newPath;
            // Mulai dari indeks 1 agar boss langsung menuju titik berikutnya, bukan mundur ke tengah sel awal
            currentPathIndex = currentPath.Count > 1 ? 1 : 0;
        }
    }

    private void HandleMovement()
    {
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
            return;

        Vector2 currentPos = rb != null ? rb.position : (Vector2)transform.position;
        Vector2 targetPosition = currentPath[currentPathIndex];
        Vector2 toTarget = targetPosition - currentPos;

        if (toTarget.magnitude > 0.12f)
        {
            Vector2 moveDir = toTarget.normalized;

            if (rb != null)
                rb.MovePosition(currentPos + moveDir * moveSpeed * Time.fixedDeltaTime);
            else
                transform.position = Vector2.MoveTowards(currentPos, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // Balik arah hadap sprite saat memutar balik mengejar player
            if (Mathf.Abs(moveDir.x) > 0.05f)
            {
                Vector3 scale = transform.localScale;
                scale.x = moveDir.x < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }
        else
        {
            currentPathIndex++;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                IDamageable player = collision.gameObject.GetComponent<IDamageable>();
                if (player != null)
                {
                    player.TakeDamage(AttackDamage);
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}