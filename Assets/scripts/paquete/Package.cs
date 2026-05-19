using UnityEngine;

public class Package : MonoBehaviour
{
    public enum TipoPaquete{
        Normal,
        Pesado,
        Fragil
    }
    Rigidbody2D rb;
    Collider2D col;

    [Header("Behavior")]
    public bool breaksOnFall;

    [Header("Throw")]
    public float throwForceMultiplier = 1f;
    private float startY;
    public TipoPaquete paquete;
    Transform carrier;

    bool canBePicked = true;

    public AudioClip lanzar;
    public AudioClip cae;
    public AudioSource aud;
    public GameObject trail;
    public float max = 16f;
    public bool broken = false;

    public ParticleSystem part;

    Vector3 startPos;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void WhatPaquete(){
        switch(paquete){
            case TipoPaquete.Normal:
                throwForceMultiplier = 1f;
                breaksOnFall = false;
                break;
                
            case TipoPaquete.Pesado:
                throwForceMultiplier = 0.75f;
                breaksOnFall = false;
                break;

            case TipoPaquete.Fragil:
                throwForceMultiplier = 1f;
                breaksOnFall = true;
                break;
        }
    }

    public void Start(){
        WhatPaquete();
        trail.SetActive(false);
        startPos = transform.position;
    }

    public void OnPickedUp(Transform holder) {
        aud.loop = false;
        aud.Stop();
        aud.clip = cae;
        aud.Play();
        if (!canBePicked) return;
        carrier = holder;
        transform.parent = holder;
        transform.localPosition = new Vector3(0f, 1.8f, 0f);
        rb.simulated = false;
        col.enabled = false;
        trail.SetActive(false);
    }
    
    public void OnDropped() {
        carrier = null;
        transform.parent = null;
        rb.simulated = true;
        col.enabled = true;
        startY = transform.position.y;
    }

    public void OnThrown(Vector2 force, Vector2 playerVelocity) {
        OnDropped();
        startY = transform.position.y;
        canBePicked = false;
        rb.linearVelocity = playerVelocity;
        rb.AddForce(force * throwForceMultiplier, ForceMode2D.Impulse);
        aud.Stop();
        aud.clip = lanzar;
        aud.Play();
        aud.loop = true;
        Invoke(nameof(EnablePickup), 0.2f);
        trail.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        float impactForce = col.relativeVelocity.magnitude;

        aud.loop = false;
        aud.Stop();
        aud.clip = cae;
        aud.Play();
        //Si choca muy fuerte se rompe
        if (breaksOnFall && impactForce > max)
        {
            Break();
        }        
    }

    public void Break() {
        if (broken) return;
        Instantiate(part, transform.position, transform.rotation);
        broken = true;
        Debug.Log("Paquete roto");
        Destroy(gameObject);
        FinalNivel.Instance.EjecutarMuerte();
    }
    void EnablePickup(){
        canBePicked = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            transform.position = startPos;
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

}
