using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class OneButton : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject howToControles;
    public AudioSource audioClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NextLevel(){
        audioClick.Play();
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceActual + 1);
    }

    public void Next(){
        audioClick.Play();
        howToPlay.SetActive(false);
        howToControles.SetActive(true);
    }
}
