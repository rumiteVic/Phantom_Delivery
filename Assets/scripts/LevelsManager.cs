using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class LevelsManager : MonoBehaviour
{
    private enum EstadoEstrellas
    {
        TodoUno,
        Mixto,
        TodoTres
    }

    private EstadoEstrellas estado;
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

    public GameObject recompensa;
    public GameObject renuncia;
    public GameObject showCer;
    public GameObject showRenun;


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
        estado = CalcularEstado();
        ActualizarUI();

    }

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
        aud.Play();
        SceneManager.LoadScene(0);
    }

    public void AbrirMenuOptions()
    {
        aud.Play();
        optionsMenu.SetActive(true);
        float savedValue = PlayerPrefs.GetFloat("masterVolume", 1f);

        volumeSlider.value = savedValue;
    }

    public void Manual()
    {
        aud.Play();
        guia.SetActive(true);
    }

    public void Instrucciones()
    {
         aud.Play();
        manual.SetActive(true);
    }

    public void Return()
    {
         aud.Play();
        optionsMenu.SetActive(false);
        guia.SetActive(false);
        manual.SetActive(false);
    }

    public void ExitManual()
    {
         aud.Play();
        optionsMenu.SetActive(false);
        guia.SetActive(false);
        manual.SetActive(false);
    }
    EstadoEstrellas CalcularEstado()
    {
        bool todosUno = true;
        bool todosTres = true;

        for (int i = 0; i < allButtons.Length; i++)
        {
            int levelNum = i + 2;
            int stars = PlayerPrefs.GetInt("Stars_Level_" + levelNum, 0);

            if (stars != 1) todosUno = false;
            if (stars != 3) todosTres = false;
        }

        if (todosTres) return EstadoEstrellas.TodoTres;
        if (todosUno) return EstadoEstrellas.TodoUno;

        return EstadoEstrellas.Mixto;
    }

    public void OcultarRecompensa()
    {
        aud.Play();
        recompensa.SetActive(false);
        renuncia.SetActive(false);
    }

    public void VerResultado2()
    {
        aud.Play();
        recompensa.SetActive(true);
    }
    public void VerResultado1()
    {
        aud.Play();
        renuncia.SetActive(true);
    }
    void ActualizarUI()
    {
        recompensa.SetActive(false);
        showCer.SetActive(false);
        showRenun.SetActive(false);
        renuncia.SetActive(false);

        switch (estado)
        {
            case EstadoEstrellas.TodoTres:

                showCer.SetActive(true);

                // Solo mostrar una vez
                if (PlayerPrefs.GetInt("CertificadoMostrado", 0) == 0)
                {
                    recompensa.SetActive(true);
                    PlayerPrefs.SetInt("CertificadoMostrado", 1);
                    PlayerPrefs.Save();
                }
                break;

            case EstadoEstrellas.TodoUno:

                showRenun.SetActive(true);

                // Solo mostrar una vez
                if (PlayerPrefs.GetInt("RenunciaMostrada", 0) == 0)
                {
                    renuncia.SetActive(true);
                    PlayerPrefs.SetInt("RenunciaMostrada", 1);
                    PlayerPrefs.Save();
                }
                break;

            case EstadoEstrellas.Mixto:
                recompensa.SetActive(false);
                showCer.SetActive(false);
                showRenun.SetActive(false);
                renuncia.SetActive(false);
                break;
        }
    }
}