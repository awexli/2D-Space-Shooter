using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenDimensions = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        screenBounds = Camera.main.ScreenToWorldPoint(screenDimensions);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        // clamp (players x position, min, max)
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x, screenBounds.x * -1);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y, screenBounds.y * -1);
    }
}
