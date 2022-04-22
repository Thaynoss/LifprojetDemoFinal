using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private string SceneToLoad;
    public GameObject MainMenuPauseUI; //fait reference a notre interface

    // active notre canvas relatif au menu de pause au démarage du jeu
    void Start()
    {
        MainMenuPauseUI.SetActive(true);
        Time.timeScale = 1f;
    }

    // on désactive notre menu de pause
    public void Resume()
    {
        MainMenuPauseUI.SetActive(false);// disable our game object
        Time.timeScale = 1f; //create slow motion in game bc of 1f.
    }

    public void Option()
    {
    }

    // permet de quitter le jeu
    public void Leave()
    {
        UnityEditor.EditorApplication.isPlaying = false; //donc le editor de jeu passe à false
        Application.Quit();
    }

    // permet de retourner au menu principal en changeant de scène
    public void Return()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);    
        }
    }

    // lance le jeu en changeant de scène pour se mettre sur la scène du jeu
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}