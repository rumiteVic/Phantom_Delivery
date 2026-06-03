using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Luciernagas : MonoBehaviour
{
    public ParticleSystem part;
    public Light2D luz;
    public Light2D luz2;
    public CircleCollider2D col;

    public float tiempoRestante = 5f;
    private float tiempoMaximo;
    public bool timerActivo = false;
    
    public float fadeSpeed = 2f;
    private float intensidadOriginal;
    private float intensidadOriginal2;
    void Start()
    {
        tiempoMaximo = tiempoRestante;
        if (luz != null) intensidadOriginal = luz.intensity;
        if (luz2 != null) intensidadOriginal2 = luz2.intensity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pakete") || collision.CompareTag("Player"))
        {
            part.Stop();
            timerActivo = true;
            tiempoRestante = tiempoMaximo;
        }
    }

    void Update()
    {
        if (timerActivo)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                luz.intensity = Mathf.MoveTowards(luz.intensity, 0, fadeSpeed * Time.deltaTime);
                luz2.intensity = Mathf.MoveTowards(luz2.intensity, 0, fadeSpeed * Time.deltaTime);
            }
            else
            {
                tiempoRestante = 0;
                timerActivo = false;
                ActivateAgain();
            }
        }
        else
        {
            if (luz != null && luz.intensity < intensidadOriginal)
            {
                luz.intensity = Mathf.MoveTowards(luz.intensity, intensidadOriginal, fadeSpeed * Time.deltaTime);
                luz2.intensity = Mathf.MoveTowards(luz2.intensity, intensidadOriginal2, fadeSpeed * Time.deltaTime);
            }
        }
    }

    void ActivateAgain()
    {
        part.Play();
    }
}