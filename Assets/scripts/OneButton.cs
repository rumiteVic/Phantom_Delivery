using UnityEngine;
using UnityEngine.SceneManagement;
public class OneButton : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject howToControles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NextLevel(){
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceActual + 1);
    }

    public void Next(){
        howToPlay.SetActive(false);
        howToControles.SetActive(true);
    }
}
