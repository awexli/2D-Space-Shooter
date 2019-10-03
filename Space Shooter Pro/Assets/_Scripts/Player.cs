using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpawnManager _spawnManager;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fireRate;
    private float _canFire;
    [SerializeField]
    private float tilt = 0;
    [SerializeField]
    private int _lives;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer = null;
    [SerializeField]
    private GameObject[] _engines = null;

    void Start()
    {
        _speed = 9.5f;
        _fireRate = 0.15f;
        _canFire = -1f;
        _lives = 3;

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
            Debug.LogError("Spawn Manager reference is null");
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

    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void ShieldPowerupDeactivate()
    {
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    IEnumerator PowerDownSpeed()
    {
        yield return new WaitForSeconds(4.5f);
        _speed = 9.5f;
    }

    public void SpeedPowerupActive()
    {
        _speed = 11.5f;
        StartCoroutine(PowerDownSpeed());
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            ShieldPowerupDeactivate();
            return;
        }
        else
        {
            _lives--;

            if (_lives == 2)
                _engines[0].SetActive(true);

            if (_lives == 1)
                _engines[1].SetActive(true);

            Debug.Log("Lives remaining: " + _lives);

            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                this.gameObject.SetActive(false);
            }
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        _spawnManager.SpawnLaser();
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

    public int GetLives()
    {
        return this._lives;
    }

}