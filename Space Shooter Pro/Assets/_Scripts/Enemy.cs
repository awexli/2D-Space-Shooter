using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test merge
public class Enemy : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Player _player;
    public float enemySpeed;
    [SerializeField]
    private UIManager _uiManager;

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
        {
            Destroy(this.gameObject);
            _player.Damage();
            _uiManager.UpdateLives();
        }

        if (other.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
            _player.ShieldPowerupDeactivate();
        }

        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            _uiManager.UpdateScore();
            Destroy(this.gameObject);
        }
    }
}
