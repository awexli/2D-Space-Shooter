using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab = null;
    [SerializeField]
    private GameObject[] _laserPrefabs = null;
    [SerializeField]
    private GameObject[] _powerupPrefabs = null;
    [SerializeField]
    private GameObject[] _containerObjects = null;
    [SerializeField]
    private Transform _laserSpawnPosition = null;
    private Enemy _enemy;
    private bool _stopSpawning = false;
    private bool _isTripleShotActive = false;

    void Start()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();

        if (_enemy == null)
            Debug.LogError("Enemy prefab is null");
    }

    public void StartSpawning()
    {
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
            GameObject newEnemy =
                Instantiate(_enemyPrefab, RandomizeSpawn(), Quaternion.identity);

            newEnemy.transform.parent = _containerObjects[1].transform;

            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawning)
        {
            int randomPowerup = Random.Range(0, 3);

            GameObject newPowerUp =
                Instantiate(_powerupPrefabs[randomPowerup], RandomizeSpawn(), Quaternion.identity);

            newPowerUp.transform.parent = _containerObjects[2].transform;

            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void SpawnLaser()
    {

        Vector3 spawnPosition = new Vector3
        (
            _laserSpawnPosition.transform.position.x,
            _laserSpawnPosition.transform.position.y,
            0
        );

        GameObject newLaser;

        if (_isTripleShotActive == true)
            newLaser = Instantiate(_laserPrefabs[1], spawnPosition, Quaternion.identity);
        else
            newLaser = Instantiate(_laserPrefabs[0], spawnPosition, Quaternion.identity);

        newLaser.transform.parent = _containerObjects[0].transform;
    }

    public void TripleShotActivate()
    {
        _isTripleShotActive = true;
    }

    public void TripleShotDeactivate()
    {
        _isTripleShotActive = false;
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
