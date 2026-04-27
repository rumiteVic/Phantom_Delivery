using UnityEngine;

public class CameraScriptX : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float blockSize = 20f; // El tamaño del salto

    private float playerStartX;
    private float cameraStartX;
    private float targetX;

    void Start()
    {
        if (player == null) return;

        // Guardamos las posiciones iniciales originales
        playerStartX = player.position.x;
        cameraStartX = transform.position.x;
        
        targetX = cameraStartX;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 1. Calculamos cuánto se ha alejado el jugador de su inicio
        float playerDeltaX = player.position.x - playerStartX;

        // 2. Calculamos en qué "bloque" está el jugador (0, 1, 2, -1, etc.)
        // Si el jugador se movió 29, esto da 0. Si se movió 31, esto da 1.
        int currentBlock = (int)(playerDeltaX / blockSize);

        // 3. El objetivo de la cámara es su inicio + (número de bloque * 30)
        targetX = cameraStartX + (currentBlock * blockSize);

        // 4. Interpolación para que el salto no sea brusco (opcional)
        float newX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}