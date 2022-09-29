using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscore_text;

    public void Awake()
    {
        int highscore = PlayerPrefs.GetInt("Highscore");
        if(highscore_text){
            highscore_text.SetText(string.Format("Highscore: {0}", highscore));
        }
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
