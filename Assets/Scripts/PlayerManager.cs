using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public string PlayerName;
    public int Score;

    public string BestScorePlayerName;
    public int BestScore;
    public string SecondScorePlayerName;
    public int SecondScore;
    public string ThirdScorePlayerName;
    public int ThirdScore;

    private string FilePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitData();
        LoadBestScore();
    }

    private void InitData()
    {
        Instance = this;
        FilePath = Application.persistentDataPath + "/bestScore.json";
    }

    public void LoadBestScore()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            if (!string.IsNullOrEmpty(json))
            {
                PlayerData[] bestScoreData = JsonHelper.FromJson<PlayerData>(json);
                List<PlayerData> scoreList = new List<PlayerData>();

                if (bestScoreData?.Length > 0)
                {
                    scoreList.AddRange(bestScoreData);
                }

                PlayerData[] scoreOrdered = scoreList
                    .OrderByDescending(x => x.Score)
                    .ToArray();

                BestScorePlayerName = !string.IsNullOrEmpty(scoreOrdered[0].PlayerName) ? scoreOrdered[0].PlayerName : PlayerName;
                BestScore = scoreOrdered[0].Score;
            }
        }

        if (string.IsNullOrEmpty(BestScorePlayerName))
        {
            BestScorePlayerName = PlayerName;
            BestScore = 0;
        }
    }

    public void SavePlayerData()
    {
        string json = File.ReadAllText(FilePath);

        if (!string.IsNullOrEmpty(json))
        {
            PlayerData[] bestScoreData = JsonHelper.FromJson<PlayerData>(json);

            List<PlayerData> newData = new List<PlayerData>();

            if (bestScoreData?.Length > 0)
            {
                newData.AddRange(bestScoreData);
            }
            newData.Add(new PlayerData
            {
                PlayerName = PlayerName,
                Score = Score
            });

            PlayerData[] dataToSave = newData
                .OrderByDescending(x => x.Score)
                .Take(3)
                .ToArray();

            string newJson = JsonHelper.ToJson<PlayerData>(dataToSave);
            File.WriteAllText(FilePath, newJson);
        }
        else
        {
            List<PlayerData> newData = new List<PlayerData>
            {
                new PlayerData
                {
                    PlayerName = PlayerName,
                    Score = Score
                }
            };

            string newJson = JsonHelper.ToJson<PlayerData>(newData.ToArray());
            File.WriteAllText(FilePath, newJson);
        }
    }

    public void LoadRanking()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            if (!string.IsNullOrEmpty(json))
            {
                PlayerData[] bestScoreData = JsonHelper.FromJson<PlayerData>(json);
                List<PlayerData> newData = new List<PlayerData>();
                
                if (bestScoreData?.Length > 0)
                {
                    newData.AddRange(bestScoreData);

                    PlayerData[] rankingData = newData
                        .OrderByDescending(x => x.Score)
                        .ToArray();

                    BestScorePlayerName = rankingData[0]?.PlayerName ?? string.Empty;
                    BestScore = rankingData[0]?.Score ?? 0;

                    SecondScorePlayerName = rankingData[1]?.PlayerName ?? string.Empty;
                    SecondScore = rankingData[1]?.Score ?? 0;

                    ThirdScorePlayerName = rankingData[2]?.PlayerName ?? string.Empty;
                    ThirdScore = rankingData[2]?.Score ?? 0;
                }
            }
        }
        else
        {
            BestScorePlayerName = string.Empty;
            BestScore = 0;
            SecondScorePlayerName = string.Empty;
            SecondScore = 0;
            ThirdScorePlayerName = string.Empty;
            ThirdScore = 0;
        }
    }

    [System.Serializable]
    private class PlayerData
    {
        public string PlayerName;
        public int Score;
    }
}
