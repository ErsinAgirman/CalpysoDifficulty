using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{   

    public void OpenGameScene()
    {
        string sceneName = "GameScene";
        SceneManager.LoadScene(sceneName); 
    }

    public void OpenInfoScene()
    {
        string sceneName = "InfoScene";
        SceneManager.LoadScene(sceneName); 
    }

    public void HowToPlayScene()
    {
        string sceneName = "HowToPlayScene";
        SceneManager.LoadScene(sceneName); 
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
