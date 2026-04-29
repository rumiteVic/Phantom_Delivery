using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float blockSize = 20f; // Ahora representa la altura del salto

    private float playerStartY;
    private float cameraStartY;
    private float targetY;

    void Start()
    {
        if (player == null) return;

        // Guardamos las posiciones iniciales originales en Y
        playerStartY = player.position.y;
        cameraStartY = transform.position.y;
        
        targetY = cameraStartY;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 1. Calculamos cuánto se ha alejado el jugador de su inicio en vertical
        float playerDeltaY = player.position.y - playerStartY;

        // 2. Calculamos en qué "bloque" de altura está el jugador
        int currentBlock = (int)(playerDeltaY / blockSize);

        // 3. El objetivo de la cámara es su inicio + (número de bloque * tamaño del bloque)
        targetY = cameraStartY + (currentBlock * blockSize);

        // 4. Interpolación suave para el movimiento vertical
        float newY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);

        // 5. Aplicamos el cambio: X y Z se mantienen, Y es la que cambia
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}