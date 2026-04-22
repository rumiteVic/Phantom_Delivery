using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class OneButton : MonoBehaviour
{
    public static OneButton Instance;
    public GameObject howToPlay;
    public GameObject howToControles;
    public AudioSource audioClick;
    
    void Awake(){
        Instance = this;
    }


    public void Tutorial(){
        howToPlay.SetActive(true);
        howToControles.SetActive(false);
    }

    public void Controles()
    {
        audioClick.Play();
        howToPlay.SetActive(false);
        howToControles.SetActive(true);
    }

    public void Manual()
    {
        audioClick.Play();
        howToPlay.SetActive(true);
    }

    public void Salirse()
    {
        audioClick.Play();
        howToPlay.SetActive(false);
        howToControles.SetActive(false);
    }
}
