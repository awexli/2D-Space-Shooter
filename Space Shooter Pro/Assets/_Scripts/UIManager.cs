using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private int _score;
    [SerializeField]
    private Image _livesImage = null;
    [SerializeField]
    private Sprite[] _liveSprites = null;
    private int _currentLives;
    [SerializeField]
    private Text _gameOverText = null;
    [SerializeField]
    private Text _restartText = null;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _currentLives = 3;
        _score = 0;
        _scoreText.text = "Score: " + _score;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (_gameManager == null)
            Debug.Log("Game Manager not found in the hierarchy");
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

        if (_currentLives < 1)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (_currentLives < 1)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
