using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    public float zoneHeight = 10f; // altura de cada "pantalla"
    public float smoothSpeed = 5f;

    private int currentZone = 0;
    private float targetY;
    public float startY = 0f;
    void Start()
    {
        transform.position = new Vector3(
            transform.position.x,
            startY,
            transform.position.z
        );

        // Zona inicial SIEMPRE la 0
        currentZone = 0;
        targetY = startY;
    }

    void LateUpdate()
{
    float upperLimit = (currentZone + 1) * zoneHeight;
    float lowerLimit = currentZone * zoneHeight;

    // SUBIR (cuando pasa arriba)
    if (player.position.y > upperLimit)
    {
        currentZone++;
        targetY = currentZone * zoneHeight;
    }
    
    else if (player.position.y < lowerLimit)
    {
        currentZone--;
        targetY = currentZone * zoneHeight;
    }

    Vector3 newPos = new Vector3(
        transform.position.x,
        Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime),
        transform.position.z
    );

    transform.position = newPos;
}
}
