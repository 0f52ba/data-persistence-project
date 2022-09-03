using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI FirstScoreText;
    public TextMeshProUGUI SecondScoreText;
    public TextMeshProUGUI ThirdScoreText;

    private void Start()
    {
        PlayerManager.Instance.LoadRanking();
        ShowRanking();
    }

    private void ShowRanking()
    {
        if (string.IsNullOrEmpty(PlayerManager.Instance.BestScorePlayerName))
        {
            FirstScoreText.text = "1. nd";
        }
        else
        {
            FirstScoreText.text = $"1. {PlayerManager.Instance.BestScorePlayerName} : {PlayerManager.Instance.BestScore}";
        }

        if (string.IsNullOrEmpty(PlayerManager.Instance.SecondScorePlayerName))
        {
            SecondScoreText.text = "2. nd";
        }
        else
        {
            SecondScoreText.text = $"2. {PlayerManager.Instance.SecondScorePlayerName} : {PlayerManager.Instance.SecondScore}";
        }

        if (string.IsNullOrEmpty(PlayerManager.Instance.ThirdScorePlayerName))
        {
            ThirdScoreText.text = "3. nd";
        }
        else
        {
            ThirdScoreText.text = $"3. {PlayerManager.Instance.ThirdScorePlayerName} : {PlayerManager.Instance.ThirdScore}";
        }
    }
}
