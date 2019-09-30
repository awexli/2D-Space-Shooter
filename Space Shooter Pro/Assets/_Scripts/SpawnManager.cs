using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private Enemy _enemy;
    private bool playerDeath = false;

    void Start()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();

        if (_enemy == null)
            Debug.LogError("Enemy reference is null");

        StartCoroutine(SpawnRoutine());
    }
    
    IEnumerator SpawnRoutine()
    {
        while (!playerDeath)
        {
            GameObject newEnemy = Instantiate (_enemyPrefab, _enemy.RandomizeSpawn(), Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(2.0f);
        }
    }

    public void OnPlayerDeath()
    {
        playerDeath = true;
    }
}
