using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LaserSpawn))]
public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _lowerBound;
    private LaserSpawn _laserSpawn;

    void Start()
    {
        _laserSpawn = GameObject.Find("Laser Spawn").GetComponent<LaserSpawn>();
        if (_laserSpawn == null)
        {
            Debug.Log("Laser Spawn reference is NULL");
        }
        transform.position = new Vector3(0, 6, 0);
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= _lowerBound)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _laserSpawn.TripleShotActive();
            Destroy(this.gameObject);
        }
    }
}
