using System.IO;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;

    public string UserName;
    public int Score;

    public string BestScoreUserName;
    public int BestScore;

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
        FilePath = Application.persistentDataPath + "/bestScoreX.json";
    }

    public void LoadBestScore()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            if (!string.IsNullOrEmpty(json))
            {
                UserData bestScoreData = JsonUtility.FromJson<UserData>(json);
                BestScoreUserName = !string.IsNullOrEmpty(bestScoreData?.UserName) ? bestScoreData.UserName : UserName;
                BestScore = bestScoreData.Score;
            }
        }

        if (string.IsNullOrEmpty(BestScoreUserName))
        {
            BestScoreUserName = UserName;
            BestScore = 0;
        }
    }

    public void SaveUserData()
    {
        var userData = new UserData
        {
            UserName = UserName,
            Score = Score
        };

        string json = JsonUtility.ToJson(userData);
        File.WriteAllText(FilePath, json);
    }

    [System.Serializable]
    private class UserData
    {
        public string UserName;
        public int Score;
    }
}
