using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public TMP_InputField NewUserName;
    public Button StartButton;

    public void Start()
    {
        UserManager.Instance.LoadBestScore();

        if (!string.IsNullOrEmpty(UserManager.Instance.BestScoreUserName))
        {
            TitleText.text = $"Best Score: {UserManager.Instance.BestScoreUserName} : {UserManager.Instance.BestScore}";
            NewUserName.text = UserManager.Instance.BestScoreUserName;
        }
        else
        {
            TitleText.text = $"Best Score: {UserManager.Instance.UserName} : 0";
        }
    }
    public void StartGame()
    {
        NewUserNameSelected();
        SceneManager.LoadScene(1);
    }

    public void NewUserNameSelected()
    {
        UserManager.Instance.UserName = string.IsNullOrEmpty(NewUserName?.text) ? "" : NewUserName.text;
        UserManager.Instance.Score = 0;
    }

    public void Exit()
    {
        UserManager.Instance.SaveUserData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
