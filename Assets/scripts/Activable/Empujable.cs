using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    Rigidbody2D rb;

    bool puedeMoverse = false;

    void Start()
    {
        puedeMoverse = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!puedeMoverse)
        {
            //rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player != null && player.repaTipo == PlayerController.Repartidor.Heavy)
        {
            puedeMoverse = true;
        }
        else
        {
           puedeMoverse = false;
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player != null && player.repaTipo == PlayerController.Repartidor.Heavy)
        {
            puedeMoverse = true;
        }
        else
        {
            puedeMoverse = false;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null && player.repaTipo == PlayerController.Repartidor.Heavy)
        {
            puedeMoverse = true;
        }
        else
        {
            puedeMoverse = false;
        }


    }
}