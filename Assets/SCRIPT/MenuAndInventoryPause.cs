using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAndInventoryPause : MonoBehaviour
{
    private string SceneToLoad;
    public GameObject menuPauseUI; //fait r�f�rence � notre interface
    public GameObject InventoryUI;

    //pour enable le script
    public GameObject Player;
    
    // au démarage on désactive le canvas d'inventaire et celui du menu de pause
    void Start()
    {
        menuPauseUI.SetActive(false);
        InventoryUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // on met à jour selon la touche appuyé on lance les différentes fonction, pour mettre le jeu en pause,
    // pour le relancer après une pause
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPauseUI.activeSelf)
            {
                Resume();
                Cursor.visible = false;
            }
            else
            {
                Pause();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(InventoryUI.activeSelf)
            {
                DesactiveInventory();
                Cursor.visible = false;
            }
            else
            {
                ActiveInventory();
            }
        }
    }

    // on relance le jeu après une pause, donc on désactive le canvas du menu pause 
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerMovementLastChance>().enabled = true;
        menuPauseUI.SetActive(false);
        Time.timeScale = 1f; 
    }

    // on met le jeu en pause et on active le canvas du menu pause
    public void Pause()
    {
        Player.GetComponent<PlayerMovementLastChance>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuPauseUI.SetActive(true);
        Time.timeScale = 0f; 
        if(InventoryUI.activeSelf)
        {
            menuPauseUI.SetActive(true);
            InventoryUI.SetActive(false);
        }
    }

    // change de scène pour passer sur celle du menu principale
    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // permet d'activer le canvas de l'inventaire et de mettre en pause le jeu
    public void ActiveInventory()
    {
        Player.GetComponent<PlayerMovementLastChance>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InventoryUI.SetActive(true);
        Time.timeScale = 0f;
        if(menuPauseUI.activeSelf)
        {
            InventoryUI.SetActive(true);
            menuPauseUI.SetActive(false);
        }
    }

    // permet de désactiver le canvas de l'inventaire et de relancer le jeu
    public void DesactiveInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerMovementLastChance>().enabled = true;
        InventoryUI.SetActive(false);
        Time.timeScale = 1f;
    }
}