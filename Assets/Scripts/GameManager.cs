using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallScript _ball;
    private int _playerScore = 0;
    private int _computerScore = 0;
    [SerializeField] private Paddle _playerPaddle;
    [SerializeField] private Paddle _computerPaddle;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _computerScoreText;

    public void AddPlayerScore()
    {
        _playerScore++;
        _playerScoreText.text = _playerScore.ToString();
        GameReset();

    }

    public void AddComputerScore()
    {
        _computerScore++;
        _computerScoreText.text = _computerScore.ToString();
        GameReset();
    }

    private void GameReset()
    {
        _ball.ResetPosition();
        _playerPaddle.ResetPaddlePosition();
        _computerPaddle.ResetPaddlePosition();
        StartCoroutine(_ball.StartingForceWait());
    }
}
