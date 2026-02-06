using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions input;

    public GameObject fantasma;
    public GameObject normal;
    public GameObject pesado;
    public GameObject escalador;

    public bool isFantasma = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        fantasma.SetActive(true);
        normal.SetActive(false);
        pesado.SetActive(false);
        escalador.SetActive(false);
    }

    void Update(){
        if(!isFantasma){
            fantasma.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(input.Player.Interact.WasPressedThisFrame() && isFantasma){

            if (col.gameObject.CompareTag("Normal")){
                fantasma.SetActive(false);
                normal.SetActive(true);
                isFantasma = false;
            }

            if (col.gameObject.CompareTag("Pesado")){
                fantasma.SetActive(false);
                pesado.SetActive(true);
                isFantasma = false;
            }

            if (col.gameObject.CompareTag("Escalador")){
                fantasma.SetActive(false);
                escalador.SetActive(true);
                isFantasma = false;
            }
        }
    }
}
