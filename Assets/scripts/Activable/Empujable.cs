using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Empujable : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player != null){
            if (player.repaTipo == PlayerController.Repartidor.Heavy){
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else{
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null){
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}