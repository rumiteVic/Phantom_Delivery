using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    private Rigidbody2D rb;

    private int empujadores = 0;

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

        return
            (player != null &&player.repaTipo == PlayerController.Repartidor.Heavy) || collision.gameObject.CompareTag("Empujable");
    }

    private void CongelarMovimiento(bool congelar)
    {
        if (congelar)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}