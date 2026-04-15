using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    Rigidbody2D rb;

    bool puedeMoverse = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!puedeMoverse)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player != null && player.repaTipo == PlayerController.Repartidor.Heavy)
        {
            puedeMoverse = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player != null)
        {
            puedeMoverse = false;
        }
    }
}