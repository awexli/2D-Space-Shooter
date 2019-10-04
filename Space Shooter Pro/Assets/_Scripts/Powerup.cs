using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private int _powerupId = 0;

    private AudioManager _audioSource;
    private Player _player;

    void Start()
    {
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            _audioSource.PlayPowerup();
            switch (_powerupId)
            {
                case 0:
                    _player.ShieldPowerupActive();
                    break;
                case 1:
                    _player.StartSpeedCoroutine();
                    break;
                case 2:
                    _player.StartTripleCoroutine();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }

    }
}
