using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _triplePrefab;
    [SerializeField]
    private bool isTripleShotActive = false;

    public void SpawnLaser()
    {
        Vector3 spawnPosition = new Vector3 
        (
            transform.position.x, 
            transform.position.y, 
            0
        );

        GameObject newLaser;

        if (isTripleShotActive == true)
            newLaser = Instantiate(_triplePrefab, spawnPosition, Quaternion.identity);
        else
            newLaser = Instantiate(_laserPrefab, spawnPosition, Quaternion.identity);

        newLaser.transform.parent = _laserContainer.transform;
    }

    IEnumerator PowerDownTripleShot()
    {
        yield return new WaitForSeconds(3.5f);
        isTripleShotActive = false;
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(PowerDownTripleShot());
    }
}
