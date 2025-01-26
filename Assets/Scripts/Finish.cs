using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject timer;  // Referencia al GameObject que tiene el script Timer

    private Timer timerScript;  // Variable para almacenar el componente Timer

    private void Start()
    {
        // Obtener el componente Timer del GameObject
        timerScript = timer.GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Llamar a la función TerminarJuego() del componente Timer
            timerScript.TerminarJuego();
            SceneManager.LoadScene("Score");
        }
    }
}
