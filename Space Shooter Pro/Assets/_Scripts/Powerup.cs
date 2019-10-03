using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0;
    private Player _player;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int powerUPID = 0;
    private AudioManager _audioSource;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        if (_spawnManager == null)
            Debug.Log("_spawnManager reference is NULL");
    }

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= Boundaries.spawnYMin)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audioSource.PlayAudio();
            switch (powerUPID)
            {
                case 0:
                    // prob move tripleshotactive to player class
                    _spawnManager.TripleShotActive();
                    break;
                case 1:
                    _player.SpeedPowerupActive();
                    break;
                case 2:
                    _player.ShieldPowerupActive();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
