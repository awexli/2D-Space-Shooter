using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 15.5f;
    Animator m_Animator;
    Collider2D m_Collider;
    private SpawnManager _spawnManger;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
        _spawnManger = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        
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
            m_Animator.SetTrigger("OnAsteroidDeath");
            m_Collider.enabled = false;
            Destroy(other.gameObject);
            _spawnManger.StartSpawning();
            Destroy(this.gameObject, 2f);
        }
    }
}
