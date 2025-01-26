using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Funci�n para cargar el nivel de juego
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1"); // Cambia "NombreDeTuEscenaDeJuego" al nombre exacto de tu escena
    }

    // Funci�n para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto se ver� en el editor pero no afectar� el juego compilado.
    }
}