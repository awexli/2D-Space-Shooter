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
    Animator m_Animator;
    Collider2D m_collider;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        m_Animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();

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
            OnPlayerCollsion();

        if (other.gameObject.tag == "Shield")
            OnShieldCollision();

        if (other.gameObject.tag == "Laser")
        {
            OnLaserCollision();
            Destroy(other.gameObject);
        }
    }

    void OnShieldCollision()
    {
        _player.ShieldPowerupDeactivate();
        EnemyDeathProtocol();
        Destroy(this.gameObject);
    }

    void OnPlayerCollsion()
    {
        _player.Damage();
        _uiManager.UpdateLives();
        EnemyDeathProtocol();
        Destroy(this.gameObject, 2.2f);
    }

    void OnLaserCollision()
    {
        _uiManager.UpdateScore();
        EnemyDeathProtocol();
        Destroy(this.gameObject, 2.2f);
    }
    
    void EnemyDeathProtocol()
    {
        m_Animator.SetTrigger("OnEnemyDeath");
        enemySpeed = 0;
        m_collider.enabled = false;
    }
}
