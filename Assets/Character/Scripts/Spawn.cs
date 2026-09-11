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

    private float currentInterval;
    private int currentLevel;


    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (EnemyRespawn.Count > 0)
            {
                int randomIndex = Random.Range(0, EnemyRespawn.Count);
                GameObject selectedEnemy = EnemyRespawn[randomIndex];

                if (selectedEnemy != null)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(-5f, 5f),
                        Random.Range(-6f, 6f),
                        0f
                    );

                    Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    public void NextLevel()
    {
        currentLevel++;

        //Mathf.Max jauh lebih efisien dan ringkas daripada Mathf.Clamp untuk kasus ini
        //karena Anda hanya perlu membatasi satu sisi saja (batas bawah/minimum).
        currentInterval = Mathf.Max(minSpawnInterval, currentInterval - intervalDecreaseRate);

        int enemyIndexToUnlock = currentLevel - 1;
        if (enemyIndexToUnlock < unlockAbleEnemies.Length)
        {
            EnemyRespawn.Add(unlockAbleEnemies[enemyIndexToUnlock]);
            Debug.Log($"Level {currentLevel}: New Enemy unlock {unlockAbleEnemies[enemyIndexToUnlock].name}");
        }

    }
}