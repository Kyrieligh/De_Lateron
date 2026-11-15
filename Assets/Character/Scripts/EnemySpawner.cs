using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 3.5f;
    [SerializeField] private float minSpawnInterval = 0.5f;
    [SerializeField] private float intervalDecreaseRate = 0.5f;

    [Header("Enemy List")]
    [SerializeField] private GameObject[] unlockAbleEnemies;
    public List<GameObject> EnemyRespawn = new List<GameObject>();

    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    private bool pendingBossSpawn = false;
    private GameObject activeBoss;

    [Header("Arena Spawn Bounds")]
    [SerializeField] private Vector2 arenaMin = new Vector2(-4f, -3f); // Batas pojok kiri-bawah dalam arena
    [SerializeField] private Vector2 arenaMax = new Vector2(4f, 3f);   // Batas pojok kanan-atas dalam arena
    [SerializeField] private LayerMask obstacleLayer;                  // Layer Wall & Object
    [SerializeField] private float checkRadius = 0.5f;                 // Radius aman dari tembok


    private float currentInterval;
    private int currentLevel;

    void Start()
    {
        currentInterval = spawnInterval;
        NextLevel();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);

            // if boss is failed to spawn, try again next interval
            if (pendingBossSpawn && activeBoss == null)
            {
                BossSpawn();
            }

            if (EnemyRespawn.Count > 0)
            {
                int randomIndex = Random.Range(0, EnemyRespawn.Count);
                GameObject selectedEnemy = EnemyRespawn[randomIndex];

                if (selectedEnemy != null && TryGetValidSpawnPosition(out Vector3 spawnPosition))
                {
                    Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    // Mencari titik acak di dalam arena yang tidak menabrak Wall atau pilar Object
    private bool TryGetValidSpawnPosition(out Vector3 validPosition)
    {
        for (int i = 0; i < 15; i++) // Maksimal 15 kali percobaan cari titik kosong
        {
            float randomX = Random.Range(arenaMin.x, arenaMax.x);
            float randomY = Random.Range(arenaMin.y, arenaMax.y);
            Vector2 candidatePos = new Vector2(randomX, randomY);

            // Jika titik ini tidak menyentuh collider Wall/Object, berarti aman untuk spawn
            if (Physics2D.OverlapCircle(candidatePos, checkRadius, obstacleLayer) == null)
            {
                validPosition = new Vector3(candidatePos.x, candidatePos.y, 0f);
                return true;
            }
        }

        validPosition = Vector3.zero;
        return false;
    }

    public void NextLevel()
    {
        currentLevel++;
        currentInterval = Mathf.Max(minSpawnInterval, currentInterval - intervalDecreaseRate);
        Debug.Log($"Spawner naik ke Level: {currentLevel}");

        if (currentLevel % 3 == 0 && activeBoss == null)
        {
            pendingBossSpawn = true;
            BossSpawn();
        }

        int enemyIndexToUnlock = currentLevel - 1;
        if (enemyIndexToUnlock < unlockAbleEnemies.Length && unlockAbleEnemies[enemyIndexToUnlock] != null)
        {
            EnemyRespawn.Add(unlockAbleEnemies[enemyIndexToUnlock]);
            Debug.Log($"Level {currentLevel}: New Enemy unlock {unlockAbleEnemies[enemyIndexToUnlock].name}");
        }
    }

    private void BossSpawn()
    {
        if (bossPrefab == null)
        {
            Debug.LogWarning("Boss gagal spawn: Boss Prefab di Inspector masih kosong (None)!");
            pendingBossSpawn = false;
            return;
        }

        if (TryGetValidSpawnPosition(out Vector3 spawnPosition))
        {
            activeBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            activeBoss.SetActive(true); // Memastikan objek boss aktif saat di-spawn
            pendingBossSpawn = false;
            Debug.Log($"Level {currentLevel} : boss has been spawned at {spawnPosition}");
        }
    }

// Menampilkan kotak hijau di Scene View untuk memudahkan pengaturan batas arena
private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((arenaMin.x + arenaMax.x) / 2f, (arenaMin.y + arenaMax.y) / 2f, 0f);
        Vector3 size = new Vector3(arenaMax.x - arenaMin.x, arenaMax.y - arenaMin.y, 0f);
        Gizmos.DrawWireCube(center, size);
    }
}