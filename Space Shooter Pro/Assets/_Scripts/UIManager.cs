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
    [SerializeField]
    private Text _gameOverText = null;
    [SerializeField]
    private Text _victoryText = null;
    [SerializeField]
    private Text _restartText = null;
    private GameManager _gameManager;
    private Player _player;

    private float timer;
    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private Text _instructionText;
    private bool _start = false;
    private bool _victory = false;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _scoreText.text = "Score: " + _score;
        _gameOverText.gameObject.SetActive(false);
        _victoryText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_gameManager == null)
            Debug.Log("Game Manager not found in the hierarchy");
    }

    void Update()
    {
        if (_player.GetLives() > 0 && _start == true)
            PlayTime();
        else
            _timerText.text = "" + timer;

        if (Input.GetKeyDown(KeyCode.Space) && _start == false)
            RemoveInstructions();
    }

    private void RemoveInstructions()
    {
        _instructionText.gameObject.SetActive(_start);
        _start = true;
    }

    private void PlayTime()
    {
        timer += Time.deltaTime;
        _timerText.text = "" + timer;
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score.ToString();

        if (_score == 200)
        {
            while (_player.GetLives() > 0)
            {
                _player.Damage();
                _victory = true;
                GameOverSequence();
            }
        }
    }

    public void UpdateLives()
    {
        _livesImage.sprite = _liveSprites[_player.GetLives()];

        if (_player.GetLives() < 1)
            GameOverSequence();
    }

    void GameOverSequence()
    {
        if (_victory == true)
            _victoryText.gameObject.SetActive(true);
        else
            _gameOverText.gameObject.SetActive(true);
            
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (_player.GetLives() < 1)
        {
            if (_victory == true)
            {
                _victoryText.text = "Victory!";
                yield return new WaitForSeconds(0.5f);
                _victoryText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                _gameOverText.text = "Game Over";
                yield return new WaitForSeconds(0.5f);
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
