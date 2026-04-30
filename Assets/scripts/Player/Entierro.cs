using UnityEngine;
using UnityEngine.Audio;
public class Entierro : MonoBehaviour
{
    private Collider2D collider;


    //Flores rosas
    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;

    //Flores azules
    public GameObject plantA1;
    public GameObject plantA2;
    public GameObject plantA3;

    //Flores moradas
    public GameObject plantM1;
    public GameObject plantM2;
    public GameObject plantM3;

    public GameObject part;
    public AudioSource aud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public void EntierraRosa() {
            aud.Play();
            plant1.SetActive(true);
            plant2.SetActive(true);
            plant3.SetActive(true);
            collider.enabled = false;
            Instantiate(part, transform.position, transform.rotation);
    }

    public void EntierraAzul() {
            aud.Play();
            plantA1.SetActive(true);
            plantA2.SetActive(true);
            plantA3.SetActive(true);
            collider.enabled = false;
            Instantiate(part, transform.position, transform.rotation);
    }

    public void EntierraMorado() {
            aud.Play();
            plantM1.SetActive(true);
            plantM2.SetActive(true);
            plantM3.SetActive(true);
            collider.enabled = false;
            Instantiate(part, transform.position, transform.rotation);
    }
}
