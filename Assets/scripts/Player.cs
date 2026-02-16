using UnityEngine;

public class Player : MonoBehaviour
{
    private InputSystem_Actions input;
    SpriteRenderer sprite;
    public PlayerController cont;

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
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Normal") || col.CompareTag("Pesado") || col.CompareTag("Escalador")){
            cont.currentBody = col;
        }
        if (cont.carriedPackage != null ||cont.isFantasma) return;
            Package pkg = col.GetComponent<Package>();

            if (pkg != null){
                cont.carriedPackage = pkg;
                cont.carriedPackage.OnPickedUp(transform);
            }
    }

    private void OnTriggerExit2D(Collider2D col){
        if (col == cont.currentBody)
            cont.currentBody = null;
    }
}