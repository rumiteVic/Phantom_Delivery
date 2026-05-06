using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class LevelsManager : MonoBehaviour
{
    public BotonSelector[] allButtons;
    public GameObject[] botonesProtocolos; 
    public GameObject[] panelesProtocolos;
    public GameObject manu;
    public GameObject selector;
    public AudioSource aud;

    public AudioMixer masterMixer;
    public GameObject optionsMenu;

    public GameObject guia;
    public GameObject manual;

    public Slider volumeSlider;


    void Awake()
    {
        float savedValue = PlayerPrefs.GetFloat("masterVolume", 1f);

        volumeSlider.value = savedValue;
        SetMasterVolume(savedValue);
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool fs = PlayerPrefs.GetInt("fullscreen") == 1;
            Screen.fullScreen = fs;
        }
    }
    
    void Start()
    {
        
        int nivelLlegado = PlayerPrefs.GetInt("LevelReached", 2);

        foreach(GameObject b in botonesProtocolos) b.SetActive(false);
        
        for (int i = 0; i < allButtons.Length; i++)
        {
            int levelNum = i + 2; 
            int stars = PlayerPrefs.GetInt("Stars_Level_" + levelNum, 0);
            bool unlocked = levelNum <= nivelLlegado;

            allButtons[i].BotonSeleccionador(levelNum, stars, unlocked);
        }

        if(nivelLlegado >= 2)
        {
            botonesProtocolos[0].SetActive(true);
            botonesProtocolos[1].SetActive(true);
            botonesProtocolos[2].SetActive(true);
            botonesProtocolos[3].SetActive(true);
        }
        if(nivelLlegado >= 3)
        {
            botonesProtocolos[4].SetActive(true);
        }
        if(nivelLlegado >= 4)
        {
            botonesProtocolos[5].SetActive(true);
        }
        if(nivelLlegado >= 7)
        {
            botonesProtocolos[6].SetActive(true);
        }
        if(nivelLlegado >= 8)
        {
            botonesProtocolos[7].SetActive(true);
        }
        optionsMenu.SetActive(false);

    }

    // Función única para mostrar protocolos (sustituye a las 7 funciones anteriores)
    public void MostrarProtocolo(int indice)
    {
        aud.Play();
        manu.SetActive(false);
        
        for (int i = 0; i < panelesProtocolos.Length; i++)
        {
            panelesProtocolos[i].SetActive(i == indice);
        }
    }

    public void SeePortadaManual()
    {
        manu.SetActive(true);
        foreach(var p in panelesProtocolos) p.SetActive(false);
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

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void AbrirMenuOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void Manual()
    {
        guia.SetActive(true);
    }

    public void Instrucciones()
    {
        manual.SetActive(true);
    }

    public void Return()
    {
        optionsMenu.SetActive(false);
        guia.SetActive(false);
        manual.SetActive(false);
    }

    public void ExitManual()
    {
        optionsMenu.SetActive(false);
        guia.SetActive(false);
        manual.SetActive(false);
    }
}