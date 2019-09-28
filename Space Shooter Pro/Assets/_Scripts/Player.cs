﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.5f;
    private int _upperBound = 0;
    private float _lowerBound = -3.8f;
    private float _rightBound = 11.3f;
    private float _leftBound = -11.3f;

    // Start is called before the first frame update
    void Start()
    {
        // take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, _lowerBound, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBounds();
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

        // Mathf.Clamp(value_to_clamp, min, max)
        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, _lowerBound, _upperBound), 0);

        if (xPosition >= _rightBound)
        {
            transform.position = new Vector3(_leftBound, yPosition, 0);
        }
        else if (xPosition <= _leftBound)
        {
            transform.position = new Vector3(_rightBound, yPosition, 0);
        }
    }
}