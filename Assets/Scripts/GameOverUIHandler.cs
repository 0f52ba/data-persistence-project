using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIHandler : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
