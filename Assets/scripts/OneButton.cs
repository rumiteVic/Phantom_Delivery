using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class OneButton : MonoBehaviour
{
    public static OneButton Instance;
    public GameObject howToPlay;
    public GameObject howToControles;
    public AudioSource audioClick;
    public float minPitch;
    public float maxPitch;

    public float volumeMin;
    public float volumeMax;

    public GameObject[] noMostrar = null;
    
    void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        audioClick= GetComponent<AudioSource>();
    }


    public void Tutorial(){
        Randomizador();
        Show();
        howToPlay.SetActive(true);
        howToControles.SetActive(false);
    }

    public void Controles()
    {
        Randomizador();
        audioClick.Play();
        howToPlay.SetActive(false);
        howToControles.SetActive(true);
    }

    public void Manual()
    {
        Randomizador();
        audioClick.Play();
        howToPlay.SetActive(true);
    }

    public void Salirse()
    {
        Randomizador();
        audioClick.Play();
        howToPlay.SetActive(false);
        howToControles.SetActive(false);
    }

    public void Randomizador()
    {
        audioClick.pitch = 1.0f + Random.Range(minPitch, maxPitch);
        audioClick.volume = 1.0f + Random.Range(volumeMin, volumeMax);  
    }

    public void Show()
    {
        if(noMostrar != null)
        {
            foreach (GameObject obj in noMostrar)
            {
                obj.SetActive(true);
            }
        }
    }
}
