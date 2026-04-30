using UnityEngine;

public class DeleteBody : MonoBehaviour
{
    public GameManager man;
    private InputSystem_Actions input;
    bool canCount = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        man = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col) {

        Debug.Log("Entró: " + col.name);
        Entierro entierro = col.GetComponent<Entierro>();
        if(col.CompareTag("Entierro")){
            if(entierro != null){
                canCount = true;
                DeleteBodyThere();
                if (CompareTag("Normal"))
                {
                    entierro.EntierraRosa();
                }
                else if (CompareTag("Escalador"))
                {
                    entierro.EntierraAzul();
                }
                else if(CompareTag("Pesado"))
                {
                    entierro.EntierraMorado();
                }
            }
        }
    }
    public void DeleteBodyThere(){
        if(canCount){
            man.UpdateBody();
        }
        Destroy(gameObject);
    }
}
