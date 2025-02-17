using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SceneTransition transition;

    public void Jugar() {
        transition.StartGame("MenuNiveles");
    }

    // Función para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto se verá en el editor pero no afectará el juego compilado.
    }

    public void Creditos()
    {
        transition.StartGame("Credits");
    }
}