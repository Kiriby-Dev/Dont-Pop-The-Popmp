using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Funci�n para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto se ver� en el editor pero no afectar� el juego compilado.
    }
}