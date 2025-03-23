using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        if(EmailAuth.Instance != null)
        {
            EmailAuth.Instance.Logout();
            Debug.LogWarning("Token silindi: "+ EmailAuth.Instance.idToken);
        }

        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene(1);
    }
}
