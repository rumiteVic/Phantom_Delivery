using UnityEngine;

public class Package : MonoBehaviour
{
    public enum TipoPaquete{
        Normal,
        Economico,
        Pesado,
        Fragil
    }

    [Header("Behavior")]
    public bool breaksOnThrow;
    public bool breaksOnFall;
    public float maxFallHeight = 5f;

    [Header("Throw")]
    public float throwForceMultiplier = 1f;

    private bool wasThrown;
    private float startY;
    public TipoPaquete paquete;

    void WhatPaquete(){
        switch(paquete){
            case TipoPaquete.Normal:
                throwForceMultiplier = 1f;
                breaksOnThrow = false;
                breaksOnFall = false;
                break;

            case TipoPaquete.Economico:
                throwForceMultiplier = 1.3f;
                breaksOnFall = false;
                breaksOnThrow = false;
                break;
                
            case TipoPaquete.Pesado:
                throwForceMultiplier = 0.5f;
                breaksOnThrow = false;
                breaksOnFall = false;
                break;

            case TipoPaquete.Fragil:
                throwForceMultiplier = 1f;
                breaksOnThrow = false;
                breaksOnFall = true;
                break;
        }
    }

    public void Start(){
        WhatPaquete();
    }

    public void OnPickedUp(Transform holder) {
        transform.parent = holder;
        transform.localPosition = new Vector3(0f, 1.8f, 0f);
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void OnThrown(Vector2 force) {
        transform.parent = null;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;

        wasThrown = true;
        startY = transform.position.y;

        rb.AddForce(force * throwForceMultiplier, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (wasThrown && breaksOnThrow) {
            Break();
            return;
        }

        if (breaksOnFall) {
            float fall = startY - transform.position.y;
            if (fall > maxFallHeight) {
                Break();
            }
        }

        wasThrown = false;
    }

    void Break() {
        Debug.Log("Paquete roto");
        Destroy(gameObject);
    }

}
