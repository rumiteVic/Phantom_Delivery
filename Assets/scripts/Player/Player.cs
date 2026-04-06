using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private InputSystem_Actions input;
    SpriteRenderer sprite;
    public PlayerController cont;

    public float pickupRadius = 1f;
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
        if (input.Player.Activate.WasPressedThisFrame()){
            if (cont.carriedPackage != null){
                cont.carriedPackage.OnDropped();
                cont.carriedPackage = null;
                return;
            }
        }

        if (!cont.isFantasma){
            Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRadius, packageLayer);
            Debug.Log("Hit: " + hit);
            if (hit != null){
                Package pkg = hit.GetComponent<Package>();
                if (pkg != null){
                    cont.carriedPackage = pkg;
                    pkg.OnPickedUp(transform);
                    cont.ApplyPackageEffects(pkg);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Normal") || col.CompareTag("Pesado") || col.CompareTag("Escalador")){
            cont.currentBody = col;
        }
        if(col.CompareTag("Agua")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if (col == cont.currentBody){
            cont.currentBody = null;
        }   
    }
}