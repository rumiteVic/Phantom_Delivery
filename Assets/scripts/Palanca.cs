using UnityEngine;

public class Palanca : MonoBehaviour
{
    private InputSystem_Actions input;
    public bool canActivar = false;
    public bool on = true;
    public Activable[] act;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    void Update(){
        if(input.Player.Activate.WasPressedThisFrame() && canActivar && on){
            foreach (var item in act)
            {
                item.Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Normal") || col.gameObject.CompareTag("Pesado") || col.gameObject.CompareTag("Escalador"))
        canActivar = true;
    }

    private void OnTriggerExit2D(Collider2D col){
        canActivar = false;
    }
}
