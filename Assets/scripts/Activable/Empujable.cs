using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    private Rigidbody2D rb;

    private int empujadores = 0;

    public bool especial = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;

        CongelarMovimiento(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EsEmpujadorValido(collision))
        {
            empujadores++;
            CongelarMovimiento(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (EsEmpujadorValido(collision))
        {
            empujadores--;

            if (empujadores <= 0)
            {
                empujadores = 0;
                CongelarMovimiento(true);
            }
        }
    }

    bool EsEmpujadorValido(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (!especial)
        {
            return (player != null &&player.repaTipo == PlayerController.Repartidor.Heavy) || collision.gameObject.CompareTag("Empujable");
        }
        else
        {
            return player != null &&player.repaTipo == PlayerController.Repartidor.Heavy;
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
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Empujable"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Si el contacto es vertical (una encima de otra)
            if (Mathf.Abs(contact.normal.y) > 0.7f)
            {
                // Evita que se transmitan fuerzas horizontales
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
        }
    }
}