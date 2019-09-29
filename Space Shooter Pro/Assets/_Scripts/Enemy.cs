using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Boundary boundary;
    public float enemySpeed;
    
    void Start()
    {
        //transform.position = new Vector3(0, boundary.yMax, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        // if bottom of screen, respawn at top with new random x pos
        if (transform.position.y <= boundary.yMin)
        {
            float randomX = Random.Range(boundary.xMin, boundary.xMax);
            
            transform.position = new Vector3
            (
                randomX,
                boundary.yMax,
                0
            );
        }

    }

    private void OnTriggerEnter(Collider other)
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
