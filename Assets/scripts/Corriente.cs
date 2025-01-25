using UnityEngine;

public class Corriente : MonoBehaviour
{
    [SerializeField] private Vector2 currentDirection = new Vector2(1, 0); // Dirección de la corriente
    [SerializeField] private float currentStrength = 10f; // Fuerza de la corriente
    [SerializeField] private GameObject player; // Referencia al jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    private void Awake()
    {
        // Obtiene el componente Rigidbody2D del jugador
        rb = player.GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            //Genera fuerza de desplazamiento en la burbuja cuando se encuentra en el area de la corriente
            rb.AddForce(currentDirection.normalized * currentStrength, ForceMode2D.Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.linearDamping = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.linearDamping = 2f;
        }
    }
}
