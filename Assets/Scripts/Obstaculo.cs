using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            Debug.Log("Colisión con el jugador detectada.");
            gameManager.GameOver();
        }
    }
}
