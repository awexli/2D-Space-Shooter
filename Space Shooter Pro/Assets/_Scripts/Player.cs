using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 9.5f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    private int _lives;
    private bool _isShieldActive = false;


    [SerializeField]
    private GameObject[] _engines = null;
    [SerializeField]
    private GameObject _explosionPrefab = null;
    [SerializeField]
    private GameObject _shieldVisualizer = null;

    [SerializeField]
    private UIManager _uiManager;
    private AudioManager _audioSource;
    private SpawnManager _spawnManager;

    void Start()
    {
        _lives = 3;

        _spawnManager = GameObject.FindGameObjectWithTag("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        if (_spawnManager == null)
            Debug.LogError("Spawn Manager reference is null");

        if (_audioSource == null)
            Debug.LogError("Audio source on the playeris NULL");
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
        if (_isShieldActive == true)
        {
            ShieldPowerupDeactivate();
            return;
        }
        else
        {
            _audioSource.PlayerHit();
            _lives--;

            if (_lives == 2)
                _engines[0].SetActive(true);

            if (_lives == 1)
                _engines[1].SetActive(true);

            Debug.Log("Lives remaining: " + _lives);

            if (_lives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                this.gameObject.SetActive(false);
            }
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        _audioSource.PlayerLaserShot();
        _spawnManager.SpawnLaser();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
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

    #region Powerups
    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
        Debug.Log("SHIELD ACTIVE");
        _shieldVisualizer.SetActive(true);
    }

    private void ShieldPowerupDeactivate()
    {
        _audioSource.ShieldBreak();
        _isShieldActive = false;
        Debug.Log("SHIELD DEACTIVATED");
        _shieldVisualizer.SetActive(false);
    }

    IEnumerator PowerupSpeed()
    {
        SetSpeed(11.5f);
        Debug.Log("SPEEDBOST ACTIVE");
        yield return new WaitForSeconds(4.5f);
        SetSpeed(9.5f);
        Debug.Log("SPEEDBOST DEACTIVED");
    }

    IEnumerator PowerupTripleShot()
    {
        _spawnManager.TripleShotActivate();
        Debug.Log("TRIPLESHOT ACTIVE");
        yield return new WaitForSeconds(3.5f);
        _spawnManager.TripleShotDeactivate();
        Debug.Log("TRIPLESHOT DEACTIVATED");
    }

    public void StartSpeedCoroutine()
    {
        StartCoroutine(PowerupSpeed());
    }

    public void StartTripleCoroutine()
    {
        StartCoroutine(PowerupTripleShot());
    }
    #endregion

    public int GetLives()
    {
        return this._lives;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy Laser")
        {
            Damage();
            _uiManager.UpdateLives();
        }

        if (other.tag == "Enemy")
        {
            Damage();
            _uiManager.UpdateLives();
        }

    }

}