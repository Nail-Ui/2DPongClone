using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
