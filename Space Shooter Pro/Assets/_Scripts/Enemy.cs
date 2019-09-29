using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Boundary boundary;
    public float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, boundary.yMax, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 meters per second
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
}
