using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform player; // El transform del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la c�mara respecto al jugador

    private Vector3 velocity = Vector3.zero; // Usado para almacenar la velocidad del movimiento

    void FixedUpdate()
    {
        // La posici�n deseada de la c�mara (con el desplazamiento)
        Vector3 desiredPosition = player.position + offset;

        // Mover la c�mara suavemente a la posici�n deseada con un retraso
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}

