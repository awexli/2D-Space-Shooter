using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildObjects : MonoBehaviour
{

    private Transform[] _child = null;
    private int _childCounter;

    void Start()
    {
        _childCounter = transform.childCount;
        _child = new Transform[_childCounter];

        Debug.Log("Child Count: " + _childCounter);
        for (int i = 0; _childCounter > 0; i++)
        {
            _child[i] = transform.GetChild(i);
            Debug.Log(_child[i].name);
            _childCounter--;
        }
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
