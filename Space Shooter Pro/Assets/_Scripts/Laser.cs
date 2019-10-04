using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _playerLaserSpeed = 10.5f;
    [SerializeField]
    private float _enemyLaserSpeed = 8.5f;
    [SerializeField]
    private int _laserID = 0;

    void Update()
    {
        if (_laserID == 0)
            MovePlayerLaser();

        if (_laserID == 1)
            MoveEnemyLaser();
    }

    private void MovePlayerLaser()
    {
        transform.Translate(Vector3.up * _playerLaserSpeed * Time.deltaTime);

        if (transform.position.y >= Boundaries.laserBound)
            Destroy(this.gameObject);
    }

    private void MoveEnemyLaser()
    {
        transform.Translate(Vector3.down * _enemyLaserSpeed * Time.deltaTime);

        if (transform.position.y <= Boundaries.spawnYMin)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && _laserID == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
