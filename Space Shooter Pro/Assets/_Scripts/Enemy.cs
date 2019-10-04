using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test merge
public class Enemy : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Player _player;
    public float enemySpeed;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _explosionPrefab = null;
    [SerializeField]
    private Transform _laserSpawn = null;
    [SerializeField]
    private GameObject _enemyLaserPrefab = null;
    private bool _isDead = false;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
            Debug.Log("Spawn Manager object is null");

        if (_player == null)
            Debug.Log("Player tag is null");

        if (_uiManager == null)
            Debug.Log("UIManager is null");
    }

    void Update()
    {
        MoveDown();
        
        if (Time.time > _canFire && !_isDead)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            SpawnLaser();
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (transform.position.y <= Boundaries.spawnYMin)
            transform.position = _spawnManager.RandomizeSpawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            EnemyDeathProtocol();

        if (other.gameObject.tag == "Shield")
            EnemyDeathProtocol();

        if (other.gameObject.tag == "Laser")
        {
            OnLaserCollision();
            Destroy(other.gameObject);
        }
    }

    void OnLaserCollision()
    {
        _uiManager.UpdateScore();
        EnemyDeathProtocol();
    }
    
    void EnemyDeathProtocol()
    {
        // put this in a container later
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _isDead = true;
        Destroy(this.gameObject);
    }

    // can move this to SpawnManager.cs
    private void SpawnLaser()
    {
        Vector3 spawnPos = new Vector3
        (
            _laserSpawn.transform.position.x,
            _laserSpawn.transform.position.y,
            0
        );

        Instantiate(_enemyLaserPrefab, spawnPos, Quaternion.identity);
    }
}
