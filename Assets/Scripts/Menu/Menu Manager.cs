using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string difficulty;


    void Start()
    {
        
    }

    void Update()
    {
        bool selectEasy = Input.GetKeyDown("f");
        bool selectHard = Input.GetKeyDown("j");
        if (selectEasy)
        {
            difficulty = "EscenaFacil";
            PlayerPrefs.SetString("SceneName", difficulty);
            SceneManager.LoadScene("EscenaPalabras");
        }
        else if (selectHard)
        {
            difficulty = "EscenaDificil";
            PlayerPrefs.SetString("SceneName", difficulty);
            SceneManager.LoadScene("EscenaPalabras");
        }
        HandleExit();
    }
     void HandleExit()
    {
        if (Input.GetKeyDown("escape")) Application.Quit();
    }
}
