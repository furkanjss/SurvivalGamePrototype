using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject foodPrefab;
    public GameObject drinkPrefab;
    public Transform playerTransform;

    public float enemySpawnInterval = 60f;
    public float itemSpawnInterval = 30f;
    public float spawnDistanceMin = 5f;
    public float spawnDistanceMax = 10f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(10);
        while (true)
        {
            SpawnAtRandomPosition(enemyPrefab);
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    IEnumerator SpawnItem()
    {
        while (true)
        {
            SpawnAtRandomPosition(foodPrefab);
            SpawnAtRandomPosition(drinkPrefab);
            yield return new WaitForSeconds(itemSpawnInterval);
        }
    }

    void SpawnAtRandomPosition(GameObject prefab)
    {
        Vector3 spawnPosition = GetRandomPositionNearPlayer();
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPositionNearPlayer()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector3 spawnPosition = playerTransform.position + randomDirection * randomDistance;
        spawnPosition.y = playerTransform.position.y; // Y eksenini aynı tutmak için
        return spawnPosition;
    }
}
