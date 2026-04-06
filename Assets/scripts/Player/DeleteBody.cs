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

        if(col.CompareTag("Entierro")){
            canCount = true;
            DeleteBodyThere();
        }
    }
    public void DeleteBodyThere(){
        if(canCount){
            man.UpdateBody();
        }
        Destroy(gameObject);
    }
}
