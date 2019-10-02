using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    private LaserSpawn _laserSpawn;
    [SerializeField]
    private float _speed;
    private float _speedBoostMultiplier;
    [SerializeField]
    private float _fireRate;
    private float _canFire;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private int _lives;

    void Start()
    {
        _speed = 9.5f;
        _fireRate = 0.15f;
        _canFire = -1f;
        _lives = 3;
        _speedBoostMultiplier = 2;

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
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

    IEnumerator PowerDownSpeed()
    {
        yield return new WaitForSeconds(4.5f);
        _speed /= _speedBoostMultiplier;
    }

    public void SpeedPowerupActive()
    {
        _speed *= _speedBoostMultiplier;
        StartCoroutine(PowerDownSpeed());
    }

    public void Damage()
    {
        _lives--;

        Debug.Log("Lives remaining: " + _lives);

        if (_lives < 1)
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