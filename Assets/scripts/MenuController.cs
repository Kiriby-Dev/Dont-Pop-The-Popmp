using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SceneTransition transition;

    public void Jugar() {
        transition.StartGame("MenuNiveles");
    }

    // Funci�n para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto se ver� en el editor pero no afectar� el juego compilado.
    }

    public void Creditos()
    {
        transition.StartGame("Credits");
    }
}