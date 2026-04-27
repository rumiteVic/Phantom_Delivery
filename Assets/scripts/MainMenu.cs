using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mensajeConfirmacion;
    public AudioSource click;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Show()
    {
        click.Play();
        mensajeConfirmacion.SetActive(true);
    }

    public void Hide()
    {   
        click.Play();
        mensajeConfirmacion.SetActive(false);
    }
}
