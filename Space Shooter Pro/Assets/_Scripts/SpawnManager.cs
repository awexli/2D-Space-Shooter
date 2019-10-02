using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerupPrefabs;
    [SerializeField]
    private GameObject[] _containerObjects;
    private Enemy _enemy;
    private bool _stopSpawning = false;

    void Start()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();

        if (_enemy == null)
            Debug.LogError("Enemy prefab is null");

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    public Vector3 RandomizeSpawn()
    {
        float randomX = Random.Range(Boundaries.spawnXMin, Boundaries.spawnXMax);
        Vector3 randomSpawn = new Vector3(randomX, Boundaries.spawnYMax, 0);
        return transform.position = randomSpawn;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, RandomizeSpawn(), Quaternion.identity);
            newEnemy.transform.parent = _containerObjects[0].transform;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            int randomPowerup = Random.Range(0, 2);
            GameObject newPowerUp = Instantiate(_powerupPrefabs[randomPowerup], RandomizeSpawn(), Quaternion.identity);
            newPowerUp.transform.parent = _containerObjects[1].transform;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
