using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private Timer timer;
    public GameObject MenuPausaPanel; // Asigna el panel del menú de pausa en el Inspector
    private bool isPaused = false;

    void Update()
    {    
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        MenuPausaPanel.SetActive(true); // Activa el menú de pausa
        Time.timeScale = 0f;             // Detiene el tiempo del juego
        isPaused = true;
        timer.PausarContador();
    }

    public void ResumeGame()
    {
        MenuPausaPanel.SetActive(false); // Desactiva el menú de pausa
        Time.timeScale = 1f;              // Restaura el tiempo del juego
        isPaused = false;
        timer.ReanudarContador();
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;               // Restaura el tiempo antes de cargar la escena
        SceneManager.LoadScene("MenuInicio"); // Cambia "MenuInicio" por el nombre de tu escena del menú principal
        timer.ReiniciarContador();
    }

    public void RestartGame()
    {
        MenuPausaPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
        timer.ReiniciarContador();
    }
}
