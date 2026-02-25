using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public bool isMainMenu;
    public int menu;

    public GameObject mainMenu;
    public GameObject hudMenu;
    public GameObject escMenu;
    public GameObject optionsMenu;
    public GameObject creditosMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isMainMenu){
            menu = 0;
            mainMenu.gameObject.SetActive(true);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(false);
            creditosMenu.gameObject.SetActive(false);
        }
        else{
            menu = 3;
            mainMenu.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(true);
            creditosMenu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isMainMenu){
            menu = 1;
        }
        if (Input.GetKeyDown(KeyCode.F1)){
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.F2)){
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.F3)){
            SceneManager.LoadScene(3);
        }
        if (menu == 0){
            //Menu home
            mainMenu.gameObject.SetActive(true);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(false);
            creditosMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else if (menu == 1){
            //EscMenu
            mainMenu.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(true);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(false);
            creditosMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else if (menu == 2){
            //OptionsMenu
            mainMenu.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);
            hudMenu.gameObject.SetActive(false);
            creditosMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else if (menu == 3){
            //HUD
            mainMenu.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(true);
            creditosMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if(menu == 4){
            mainMenu.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            hudMenu.gameObject.SetActive(false);
            creditosMenu.gameObject.SetActive(true);
        }
    }

    public void Iniciar(){
        SceneManager.LoadScene(1);
    }

    public void Opciones(){
        menu = 2;
    }
    public void Salir(){
        Application.Quit();
    }

    public void Return(){
        if(isMainMenu)
        {
            menu = 0;
        }
        else
        {
            menu = 1;
        }
    }

    public void Resume(){
        menu = 3;
    }

    public void ExitGame(){
        SceneManager.LoadScene(0);
    }
    public void Creditos(){
        menu = 4;
    }

    public void ReturnMainMenu(){
        menu = 0;
        SceneManager.LoadScene(0);
    }
}
