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
    private Enemy _enemy;
    private bool playerDeath = false;

    void Start()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();

        if (_enemy == null)
            Debug.LogError("Enemy reference is null");

        StartCoroutine(SpawnRoutine());
    }

    public Vector3 RandomizeSpawn()
    {
        float randomX = Random.Range(Boundaries.spawnXMin, Boundaries.spawnXMax);
        Vector3 randomSpawn = new Vector3(randomX, Boundaries.spawnYMax, 0);

        return transform.position = randomSpawn;
    }
    
    IEnumerator SpawnRoutine()
    {
        while (!playerDeath)
        {
            GameObject newEnemy = Instantiate (_enemyPrefab, RandomizeSpawn(), Quaternion.identity);
            //GameObject newTripleShotPowerup = Instantiate (_tripleShotPowerupPrefab, RandomizeSpawn(), Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(2.0f);
        }
    }

    public void OnPlayerDeath()
    {
        playerDeath = true;
    }
}
