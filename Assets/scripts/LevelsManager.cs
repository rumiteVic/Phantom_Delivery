using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelsManager : MonoBehaviour
{
    public BotonSelector[] allButtons;
    public AudioSource aud;

    public GameObject menuOptions;

    public GameObject howToJuego;
    public GameObject howToPlay;

    public AudioMixer masterMixer;
    public Slider volumeSlider;
    public GameObject selector;

    public GameObject button1;
    public GameObject button2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int nivelLlegado = PlayerPrefs.GetInt("LevelReached", 2);
        for (int i = 0; i < allButtons.Length; i++)
        {
            int levelNum = i + 2;
            int stars = PlayerPrefs.GetInt("Stars_Level_" + levelNum, 0);
            bool unlocked = levelNum <= nivelLlegado;

            allButtons[i].BotonSeleccionador(levelNum, stars, unlocked);
        }
        float savedValue = PlayerPrefs.GetFloat("masterVolume", 1f);

        volumeSlider.value = savedValue;
        SetMasterVolume(savedValue);
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool fs = PlayerPrefs.GetInt("fullscreen") == 1;
            Screen.fullScreen = fs;
        }

        bool tutorialHecho = PlayerPrefs.GetInt("TutorialCompletado", 0) == 1;
        
        button1.SetActive(tutorialHecho);
        button2.SetActive(tutorialHecho);
    }

    public void Home()
    {
        aud.Play();
        SceneManager.LoadScene(0);
    }

    public void AbrirMenuOptions()
    {
        aud.Play();
        menuOptions.SetActive(true);
        selector.SetActive(false);
    }

    public void Manual()
    {
        aud.Play();
        howToJuego.SetActive(true);
        howToPlay.SetActive(false);
        selector.SetActive(false);
    }
    public void Instrucciones()
    {
        aud.Play();
        howToJuego.SetActive(false);
        howToPlay.SetActive(true);
        selector.SetActive(false);
    }

    public void Return()
    {
        aud.Play();
        howToPlay.SetActive(false);
        howToJuego.SetActive(false);
        menuOptions.SetActive(false);
        selector.SetActive(true);
    }

    public void SetMasterVolume(float value){
        float rawValue = value;

        value = Mathf.Clamp(value, 0.0001f, 1f);
        value = Mathf.Pow(value, 2f);

        float volume = Mathf.Log10(value) * 20f;

        masterMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("masterVolume", rawValue);
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
