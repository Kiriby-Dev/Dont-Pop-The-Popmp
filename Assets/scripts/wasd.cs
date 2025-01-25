using UnityEngine;

public class WASD : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del objeto
    [SerializeField] private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    void Update()
    {
        // Leer entrada del teclado
        float movimientoHorizontal = Input.GetAxis("Horizontal"); // A y D (o flechas izquierda/derecha)
        float movimientoVertical = Input.GetAxis("Vertical");     // W y S (o flechas arriba/abajo)

        // Calcular la dirección del movimiento
        Vector3 direccion = new Vector3(movimientoHorizontal, movimientoVertical, 0f);

        // Mover el objeto
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

    }

    private void Start()
    {
        rb.linearDamping = 2f;
    }
}
