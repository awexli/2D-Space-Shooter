using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private int _score;
    private bool _isEnemyKilled = false;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        //assign text component to the handle
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score.ToString();
    }
}
