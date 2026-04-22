using UnityEngine;

public class BotonActivable : MonoBehaviour
{
    public Activable[] act;

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Empujable") ||col.gameObject.CompareTag("Pakete")){
            ActivarCosas();
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        ActivarCosas();
    }

    void ActivarCosas(){
        foreach (var item in act){
            item.Activate();
        }
    }
}
