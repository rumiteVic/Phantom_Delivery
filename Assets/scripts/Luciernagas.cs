using UnityEngine;

public class Luciernagas : MonoBehaviour
{
    public ParticleSystem part;
    public CircleCollider2D col;

    public float tiempoRestante = 5f;
    public bool timerActivo = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pakete") || collision.CompareTag("Player"))
        {
            var main = part.main;
            main.loop = false;
            timerActivo = true;
        }
         
    }

    void Update()
    {
        if (timerActivo)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
            }
            else
            {
                tiempoRestante = 0;
                timerActivo = false;
                ActivateAgain();
            }
        }
    }

    void ActivateAgain()
    {
        var main = part.main;
        main.loop = true;
        part.Play();
    }
}
