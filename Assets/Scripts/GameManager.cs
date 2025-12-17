using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("GamePlay")]
    [SerializeField] private BallScript _ball;
    [SerializeField] private Paddle _playerPaddle;
    [SerializeField] private Paddle _computerPaddle;

    [Header("Score")]
    [SerializeField] private int _winScore = 5;
    private int _playerScore = 0;
    private int _computerScore = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _computerScoreText;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameoverText;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private bool _isGameOver = false;

    private void Start()
    {
        _playerScore = 0;
        _computerScore = 0;

        UpdateScoreUI();

        SetGameOverUI(false);

        _restartButton.onClick.AddListener(RestartGame);
        _mainMenuButton.onClick.AddListener(GoToMainMenu);

        Time.timeScale = 1f;
    }

    public void AddPlayerScore()
    {
        if (_isGameOver) return;

        _playerScore++;
        UpdateScoreUI();

        if (CheckGameOver()) return;

        RoundReset();

    }

    public void AddComputerScore()
    {
        if (_isGameOver) return;

        _computerScore++;
        UpdateScoreUI();

        if (CheckGameOver()) return;

        RoundReset();
    }

    private void RoundReset()
    {
        _ball.ResetPosition();
        _playerPaddle.ResetPaddlePosition();
        _computerPaddle.ResetPaddlePosition();
        StartCoroutine(_ball.StartingForceWait());
    }

    private void UpdateScoreUI()
    {
        _playerScoreText.text = _playerScore.ToString();
        _computerScoreText.text = _computerScore.ToString();
    }

    private bool CheckGameOver()
    {
        if (_playerScore >= _winScore)
        {
            TriggerGameOver("You Win");
            _gameoverText.color = Color.green;

            return true;
        }

        if (_computerScore >= _winScore)
        {
            TriggerGameOver("You Lose");
            _gameoverText.transform.position = new Vector2(990,800);
            _gameoverText.color = Color.red;
            return false;
        }

        return false;
    }

    private void TriggerGameOver(string message)
    {
        _isGameOver = true;
        _gameoverText.text = message;
        SetGameOverUI(true);
        Time.timeScale = 0f;
    }

    private void SetGameOverUI(bool isVisible)
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(isVisible);
        }
        else
        {
            _restartButton.gameObject.SetActive(isVisible);
            _mainMenuButton.gameObject.SetActive(isVisible);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
