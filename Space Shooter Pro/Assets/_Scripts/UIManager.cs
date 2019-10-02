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
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    private int _currentLives;

    // Start is called before the first frame update
    void Start()
    {
        _currentLives = 3;
        _score = 0;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score.ToString();
    }

    public void UpdateLives()
    {
        _currentLives--;
        _livesImage.sprite = _liveSprites[_currentLives];
    }
}
