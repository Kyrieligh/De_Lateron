using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 3.5f;

    [Header("Enemy List")]
    public List<GameObject> EnemyRespawn = new List<GameObject>();

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
}