using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text PlayerText;
    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverMenu;
    public Text PlayerScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        ShowPlayerData();
        ShowBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverMenu.SetActive(true);
        PlayerScoreText.text = $"Your score: {m_Points}";

        SaveScore();
        ShowBestScore();
    }

    private void SaveScore()
    {
        PlayerManager.Instance.Score = m_Points;

        if (PlayerManager.Instance.Score >= PlayerManager.Instance.BestScore ||
            PlayerManager.Instance.Score >= PlayerManager.Instance.SecondScore ||
            PlayerManager.Instance.Score >= PlayerManager.Instance.ThirdScore)
        {
            PlayerManager.Instance.SavePlayerData();
        }
    }

    private void ShowPlayerData()
    {
        PlayerText.text = $"Player: {PlayerManager.Instance.PlayerName}";
        ScoreText.text = $"Score: 0";
    }

    public void ShowBestScore()
    {
        PlayerManager.Instance.LoadBestScore();
        BestScoreText.text = $"{PlayerManager.Instance.BestScorePlayerName} : {PlayerManager.Instance.BestScore}";
    }
}
