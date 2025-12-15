using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallScript _ball;
    private int _playerScore = 0;
    private int _computerScore = 0;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _computerScoreText;

    private void Start()
    {
        _playerScoreText.text = _playerScore.ToString();
        _computerScoreText.text = _computerScore.ToString();

    }

    public void AddPlayerScore()
    {
        _playerScore++;

        _playerScoreText.text = _playerScore.ToString();
        Debug.Log("Player Scored " + _playerScore);

        _ball.ResetPosition();
    }

    public void AddComputerScore()
    {
        _computerScore++;
        _computerScoreText.text = _computerScore.ToString();
        Debug.Log("Computer Scored " + _computerScore);

        _ball.ResetPosition();
    }
}
