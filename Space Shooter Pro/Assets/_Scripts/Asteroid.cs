using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 15.5f;
    [SerializeField]
    private GameObject _explosionPrefab = null;
    private SpawnManager _spawnManger;
    private AudioManager _audioSource;

    void Start()
    {
        _spawnManger = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        if (_spawnManger == null)
            Debug.Log("Spawn Manager reference is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            //_audioSource.PlayExplosion();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManger.StartSpawning();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
