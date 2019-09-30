using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary 
{
    // yMax = 0f
    // yMin = 3.8f
    // xMax = 11.3f
    // xMin = -11.3f
    public float yMax, yMin, xMax, xMin;
}

public class Player : MonoBehaviour
{
    public Boundary boundary;
    [SerializeField]
    private float _speed = 9.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private int lives = 3;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager reference is null");
        }
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

        if (lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    
    void FireLaser()
    {
        // Could also make a child laserSpawn object to player
        // position laserSpawn object in front of player
        // use the transform.position of laserSpawn
        Vector3 laserSpawn = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, laserSpawn, Quaternion.identity);
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
            Mathf.Clamp(yPosition, boundary.yMin, boundary.yMax), 
            0
        );

        // Loop x boundaries
        if (xPosition >= boundary.xMax)
        {
            transform.position = new Vector3(boundary.xMin, yPosition, 0);
        }
        else if (xPosition <= boundary.xMin)
        {
            transform.position = new Vector3(boundary.xMax, yPosition, 0);
        }
    }
}