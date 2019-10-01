using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpawnManager _spawnManager;
    public float enemySpeed;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        // if bottom of screen, respawn at top with new random x pos
        if (transform.position.y <= Boundaries.spawnYMin)
        {
           _spawnManager.RandomizeSpawn();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            // make sure player script is set as a component
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
