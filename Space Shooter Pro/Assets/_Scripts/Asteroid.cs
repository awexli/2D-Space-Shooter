using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 15.5f;
    Animator m_Animator;
    Collider2D m_Collider;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
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
            Destroy(this.gameObject, 2f);
        }
    }
}
