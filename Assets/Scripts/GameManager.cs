using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _opponentScoreText;
    [SerializeField] private BallController _ballController;

    private int _playerScore;
    private int _opponentScore;

    private int PlayerScore
    {
        get => _playerScore;
        set
        {
            _playerScore = value;
            _playerScoreText.text = _playerScore.ToString();
        }
    }

    private int OpponentScore
    {
        get => _opponentScore;
        set
        {
            _opponentScore = value;
            _opponentScoreText.text = _opponentScore.ToString();
        }
    }

    private void Start()
    {
        PlayerScore = 0;
        OpponentScore = 0;
        // RefreshTheScoreText();
    }

    public void AddScore(bool _isPlayer, int amount = 1)
    {
        if (_isPlayer)
        {
            PlayerScore += amount;
        }
        else
        {
            OpponentScore += amount;
        }
        // RefreshTheScoreText();
    }


    // private void RefreshTheScoreText()
    // {
    //     _playerScoreText.text = _playerScore.ToString();
    //     _opponentScoreText.text = _opponentScore.ToString();
    // }

    public void ResetBall()
    {
        StartCoroutine(_ballController.ResetRoutine());
    }
}
