using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private Timer timer;
    public GameObject MenuPausaPanel; // Asigna el panel del menú de pausa en el Inspector
    private bool isPaused = false;
    private bool preventClick = false; // Nueva bandera para evitar clics

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
        PlayerPrefs.SetInt("Paused", 1);
        MenuPausaPanel.SetActive(true); // Activa el menú de pausa
        Time.timeScale = 0f;            // Detiene el tiempo del juego
        isPaused = true;
        timer.PausarContador();
    }

    public void ResumeGame()
    {
        PlayerPrefs.SetInt("Paused", 0);
        MenuPausaPanel.SetActive(false); // Desactiva el menú de pausa
        Time.timeScale = 1f;             // Restaura el tiempo del juego
        isPaused = false;
        timer.ReanudarContador();

        preventClick = true; // Activa la bandera para ignorar clics
        Invoke(nameof(ResetPreventClick), 0.1f); // Espera 0.1 segundos para permitir clics
    }

    private void ResetPreventClick()
    {
        preventClick = false; // Restablece la capacidad de hacer clic
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.SetInt("Paused", 0);
        Time.timeScale = 1f;                // Restaura el tiempo antes de cargar la escena
        SceneManager.LoadScene("MenuInicio"); // Cambia "MenuInicio" por el nombre de tu escena del menú principal
        timer.ReiniciarContador();
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Paused", 0);
        MenuPausaPanel.SetActive(false);
        SceneManager.LoadScene("Nivel1");
        Time.timeScale = 1f;
        isPaused = false;
        timer.ReiniciarContador();
        PlayerPrefs.SetInt("CantidadIntentos", 0);
        PlayerPrefs.Save();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool PreventClick() // Método para consultar el estado de preventClick
    {
        return preventClick;
    }
}
