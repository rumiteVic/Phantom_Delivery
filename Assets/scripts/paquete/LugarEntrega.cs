using UnityEngine;
using UnityEngine.Audio;
public class LugarEntrega : MonoBehaviour
{

    private Collider2D colliderT;
    public GameManager man;

    public GameObject entre;
    public GameObject noEntre;
    public GameObject flecha;
    public GameObject part;
    public AudioSource aud;

    void Start(){
        colliderT = GetComponent<Collider2D>();
        man = GameManager.Instance;
        entre.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Pakete")){

            Package paquete = col.GetComponent<Package>();
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            //Si paquete es fragil y entra muy fuertemente
            //Paquete se rompe :(
            if (paquete != null && rb != null)
            {
                float speed = rb.linearVelocity.magnitude;

                if (paquete.breaksOnFall && speed > paquete.max)
                {
                    paquete.Break();
                    return;
                }
            }

            // Entrega normal
            aud.Play();
            man.UpdatePakage();
            Destroy(col.gameObject);

            entre.SetActive(true);
            noEntre.SetActive(false);
            colliderT.enabled = false;
            flecha.SetActive(false);
            Instantiate(part, transform.position, transform.rotation);
        }
    }
}
