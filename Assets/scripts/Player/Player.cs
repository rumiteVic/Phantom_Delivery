using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private InputSystem_Actions input;
    SpriteRenderer sprite;
    public PlayerController cont;

    public float pickupRadius = 1f;
    public LayerMask packageLayer;
    
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        anim.SetFloat("Speed", Mathf.Abs(cont.move.x));
        anim.SetBool("IsGrounded", cont.grounded);
        anim.SetBool("IsClimbing", cont.isClimbing);
        anim.SetFloat("VelY", cont.rb.linearVelocity.y);
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

        if (!cont.isFantasma && cont.carriedPackage == null){
            Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRadius, packageLayer);
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