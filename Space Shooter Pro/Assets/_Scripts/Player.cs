using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    private LaserSpawn _laserSpawn;
    [SerializeField]
    private float _speed = 9.5f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private int lives = 3;


    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _laserSpawn = GameObject.Find("Laser Spawn").GetComponent<LaserSpawn>();

        if (_spawnManager == null)
            Debug.LogError("Spawn Manager reference is null");
        if (_laserSpawn == null)
            Debug.LogError("Laser Spawn reference is null");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayerBounds();
    }

    public void Damage()
    {
        lives--;

        Debug.Log("Lives remaining: " + lives);

        if (lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        _laserSpawn.SpawnLaser();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // Player tilt
        transform.rotation = Quaternion.Euler
        (
            0.0f,
            horizontalInput * -tilt,
            0.0f
        );
    }

    void PlayerBounds()
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;

        // Clamp y boundaries
        // Mathf.Clamp(value_to_clamp, min, max)
        transform.position = new Vector3
        (
            xPosition,
            Mathf.Clamp(yPosition, Boundaries.playerYMin, Boundaries.playerYMax),
            0
        );

        // Loop x boundaries
        if (xPosition >= Boundaries.playerXMax)
            transform.position = new Vector3(Boundaries.playerXMin, yPosition, 0);
        else if (xPosition <= Boundaries.playerXMin)
            transform.position = new Vector3(Boundaries.playerXMax, yPosition, 0);
    }
}