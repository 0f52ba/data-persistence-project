using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject NameTextError;
    public TMP_InputField PlayerName;

    public void Start()
    {
        NameTextError.SetActive(false);
    }

    public void StartGame()
    {
        var isNameValid = ValidatePlayerName();

        if(isNameValid)
        {
            PlayerManager.Instance.PlayerName = PlayerName.text;
            PlayerManager.Instance.Score = 0;

            SceneManager.LoadScene(1);
        }
    }

    public bool ValidatePlayerName()
    {
        if (string.IsNullOrEmpty(PlayerName?.text))
        {
            NameTextError.SetActive(true);
            return false;
        }
        else
        {
            NameTextError.SetActive(false);
            return true;
        }
    }

    public void ShowRanking()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        PlayerManager.Instance.SavePlayerData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
