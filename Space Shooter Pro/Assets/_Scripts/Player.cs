using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.5f;
    private int _upperBound = 0;
    private float _lowerBound = -3.8f;
    private int _rightBound = 11;
    private int _leftBound = -11;

    // Start is called before the first frame update
    void Start()
    {
        // take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, _lowerBound, 0);
    }

    // Update is called once per frame
    void Update()
    {

        #region Player Movements
        // new Vector3(3.5, 0, 0) * horizontalInput * _speed * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        #endregion

        #region Player Bounds
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;

        if (yPosition >= _upperBound) 
        {
            transform.position = new Vector3(xPosition, _upperBound, 0);
        } else if (yPosition <= _lowerBound)
        {
            transform.position = new Vector3(xPosition, _lowerBound, 0);
        }

        if (xPosition >= _rightBound) 
        {
            transform.position = new Vector3(_leftBound, yPosition, 0);
        } else if (xPosition <= _leftBound) 
        {
            transform.position = new Vector3(_rightBound, yPosition, 0);
        }
        #endregion

    }
}
