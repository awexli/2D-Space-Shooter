using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private LaserSpawn _laserSpawn;
    private Player _player;
    [SerializeField]
    private int powerUPID;

    void Start()
    {
        _laserSpawn = GameObject.Find("Laser Spawn").GetComponent<LaserSpawn>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_laserSpawn == null)
            Debug.Log("Laser Spawn reference is NULL");
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= Boundaries.spawnYMin)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (powerUPID)
            {
                case 0:
                // prob move tripleshotactive to player class
                    _laserSpawn.TripleShotActive();
                    break;
                case 1:
                    _player.SpeedPowerupActive();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
