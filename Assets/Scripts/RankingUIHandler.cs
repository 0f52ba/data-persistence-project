using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingUIHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
