using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Función para cargar el nivel de juego
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1"); // Cambia "NombreDeTuEscenaDeJuego" al nombre exacto de tu escena
        PlayerPrefs.SetInt("CantidadIntentos", 0);
        PlayerPrefs.SetInt("Paused", 0);
        PlayerPrefs.Save();
    }

    // Función para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto se verá en el editor pero no afectará el juego compilado.
    }
}