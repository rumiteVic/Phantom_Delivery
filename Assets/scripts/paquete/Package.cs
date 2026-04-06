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
    public float maxFallHeight = 1f;

    [Header("Throw")]
    public float throwForceMultiplier = 1f;
    private float startY;
    public TipoPaquete paquete;
    Transform carrier;

    bool canBePicked = true;
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
    }

    public void OnPickedUp(Transform holder) {
        if (!canBePicked) return;
        carrier = holder;
        transform.parent = holder;
        transform.localPosition = new Vector3(0f, 1.8f, 0f);
        rb.simulated = false;
        col.enabled = false;
    }
    
    public void OnDropped() {
        carrier = null;
        transform.parent = null;
        rb.simulated = true;
        col.enabled = true;
    }

    public void OnThrown(Vector2 force, Vector2 playerVelocity) {
        OnDropped();
        startY = transform.position.y;
        canBePicked = false;
        rb.linearVelocity = playerVelocity;
        rb.AddForce(force * throwForceMultiplier, ForceMode2D.Impulse);
        Invoke(nameof(EnablePickup), 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (breaksOnFall) {
            float fall = startY - transform.position.y;
            if (fall > maxFallHeight) {
                Break();
            }
        }
    }

    void Break() {
        Debug.Log("Paquete roto");
        Destroy(gameObject);
    }
    void EnablePickup(){
        canBePicked = true;
    }   

}
