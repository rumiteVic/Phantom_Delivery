using UnityEngine;

public class LugarEntrega : MonoBehaviour
{

    private Collider2D collider;
    public GameManager man;

    public GameObject entre;
    public GameObject noEntre;
    public GameObject flecha;
    public GameObject part;

    void Start(){
        collider = GetComponent<Collider2D>();
        man = GameManager.Instance;
        entre.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Pakete")){
            man.UpdatePakage();
            Destroy(col.gameObject);
            entre.SetActive(true);
            noEntre.SetActive(false);
            collider.enabled = false;
            flecha.SetActive(false);
            Instantiate(part, transform.position, transform.rotation);
        }
    }
}
