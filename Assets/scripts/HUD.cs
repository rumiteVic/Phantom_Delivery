using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    public AudioSource audioClick;

    public AudioMixer masterMixer;
    public Slider volumeSlider;

    public GameObject cont;
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
        float savedValue = PlayerPrefs.GetFloat("masterVolume", 1f);

        volumeSlider.value = savedValue;
        SetMasterVolume(savedValue);
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool fs = PlayerPrefs.GetInt("fullscreen") == 1;
            Screen.fullScreen = fs;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isMainMenu){
            menu = 1;
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
        audioClick.Play();
        SceneManager.LoadScene(1);
    }

    public void Opciones(){
        audioClick.Play();
        menu = 2;
    }
    public void Salir(){
        audioClick.Play();
        Application.Quit();
    }

    public void Return(){
        audioClick.Play();
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
        audioClick.Play();
        menu = 3;
        Time.timeScale = 1f;
    }

    public void ExitGame(){
        audioClick.Play();
        SceneManager.LoadScene(0);
    }
    public void Creditos(){
        audioClick.Play();
        menu = 4;
    }

    public void ReturnMainMenu(){
        audioClick.Play();
        menu = 0;
        SceneManager.LoadScene(0);
    }

    public void Retry(){
        audioClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseMenu(){
        audioClick.Play();
        menu = 1;
        Time.timeScale = 0f;
    }

    public void SetMasterVolume(float value){
        float rawValue = value;

        value = Mathf.Clamp(value, 0.0001f, 1f);
        value = Mathf.Pow(value, 2f);

        float volume = Mathf.Log10(value) * 20f;

        masterMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("masterVolume", rawValue);
    }

    public void NextLevel(){
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceActual + 1);
    }

    public void ControlsMenu(){
        cont.SetActive(true);
    }

    public void BackCont(){
        cont.SetActive(false);
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
