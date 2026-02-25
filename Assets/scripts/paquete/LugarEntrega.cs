using UnityEngine;

public class LugarEntrega : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Pakete")){
            Destroy(col.gameObject);
        }
    }
}
