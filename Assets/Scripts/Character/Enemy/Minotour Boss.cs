using System.Collections.Generic;
using UnityEngine;

public class MinotourBoss : Enemy
{
    [Header("A* Pathfinding")]
    public GridPathfinding pathfinding; // Referensi ke sistem A* di Tilemaps

    private List<Vector2> currentPath;
    private int currentPathIndex;
    private float pathUpdateTimer = 0.5f; // Re-pathing setiap 0.5 detik
    private float nextDamageTime;

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
        if (Player.Instance == null || pathfinding == null) return;

        Vector2 playerPos = Player.Instance.transform.position;
        Vector2 myPos = transform.position;

        currentPath = pathfinding.FindPathWorld(myPos, playerPos);
        currentPathIndex = 0;
    }

    private void HandleMovement()
    {
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
            return;

        Vector3 targetPosition = currentPath[currentPathIndex];

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            currentPathIndex++;
        }
    }
}