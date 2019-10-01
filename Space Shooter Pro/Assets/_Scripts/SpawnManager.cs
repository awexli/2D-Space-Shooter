using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _powerUpContainer;
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
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            GameObject newTriple = Instantiate(_tripleShotPowerupPrefab, RandomizeSpawn(), Quaternion.identity);
            newTriple.transform.parent = _powerUpContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
