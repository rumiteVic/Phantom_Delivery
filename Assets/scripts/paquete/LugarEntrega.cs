using UnityEngine;

public class LugarEntrega : MonoBehaviour
{

    public GameManager man;

    void Start(){
        man = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Pakete")){
            man.UpdatePakage();
            Destroy(col.gameObject);
        }
    }
}
