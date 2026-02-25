using UnityEngine;

public class Player : MonoBehaviour
{
    private InputSystem_Actions input;
    SpriteRenderer sprite;
    public PlayerController cont;

    public float pickupRadius = 0.5f;
    public LayerMask packageLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        if(cont.move.x >0){
            sprite.flipX = false;
        }
        if(cont.move.x < 0){
            sprite.flipX = true;
        }
        if (cont.currentBody != null && cont.isFantasma && input.Player.Interact.WasPressedThisFrame()){
            cont.CambiarCuerpo(cont.currentBody);
        }
        if (input.Player.Activate.WasPressedThisFrame()){
            if (cont.carriedPackage != null){
                cont.carriedPackage.OnDropped();
                cont.carriedPackage = null;
                return;
            }
            if (cont.isFantasma) return;

            Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRadius, packageLayer);
            if (hit != null){
                Package pkg = hit.GetComponent<Package>();
                if (pkg != null){
                    cont.carriedPackage = pkg;
                    pkg.OnPickedUp(transform);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Normal") || col.CompareTag("Pesado") || col.CompareTag("Escalador")){
            cont.currentBody = col;
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if (col == cont.currentBody){
            cont.currentBody = null;
        }   
    }
}