using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // El objeto debe ser dinámico para que la gravedad le afecte siempre.
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        // Por defecto, congelamos la posición X y la rotación. 
        // El eje Y se queda libre para que pueda caer.
        CongelarMovimiento(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluarEmpuje(collision, true);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        EvaluarEmpuje(collision, false);
    }

    private void EvaluarEmpuje(Collision2D collision, bool tocando)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if ((player != null && player.repaTipo == PlayerController.Repartidor.Heavy) || collision.gameObject.CompareTag("Empujable"))
        {
            // Si el Heavy lo toca, liberamos X. Si lo deja de tocar, lo bloqueamos.
            CongelarMovimiento(!tocando);
        }
    }

    private void CongelarMovimiento(bool congelar)
    {
        if (congelar)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            // Solo congelamos la rotación para que el objeto no ruede como una pelota
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}