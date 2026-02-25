using UnityEngine;

public class DeleteBody : MonoBehaviour
{
    private InputSystem_Actions input;
    bool canDelete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(canDelete && input.Player.Interact.WasPressedThisFrame()){
            DeleteBodyThere();
        } 
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Fantasma")){
            canDelete = true;
        }
        else if(col.CompareTag("Entierro")){
            DeleteBodyThere();
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        canDelete = false;
    }

    void DeleteBodyThere(){
        Destroy(gameObject, 0.01f);
    }
}
