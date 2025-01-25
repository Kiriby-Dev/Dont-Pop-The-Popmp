using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int tiempoEnSegundos = 0; // Contador de segundos
    private bool contar = true; // Bandera para detener/reanudar el contador
    private TMP_Text textoTiempo;  // Referencia a un objeto UI para mostrar el tiempo (opcional)

    private void Awake()
    {
        textoTiempo = GetComponent<TMP_Text>();
    }

    void Start()
    {
        // Inicia el contador as�ncrono
        IniciarContador();
    }

    // M�todo principal del contador usando async/await
    private async void IniciarContador()
    {
        while (Application.isPlaying)
        {
            if (contar)
            {
                tiempoEnSegundos++;

                // Actualiza el texto de la UI si est� configurado
                if (textoTiempo != null)
                {
                    textoTiempo.text = (tiempoEnSegundos / 100f).ToString();
                }
            }

            // Espera 1 segundo antes de continuar
            await Task.Delay(10);
        }
    }

    // M�todos para controlar el contador
    public void PausarContador()
    {
        contar = false;
    }

    public void ReanudarContador()
    {
        contar = true;
    }

    public void ReiniciarContador()
    {
        tiempoEnSegundos = 0;

        // Opcional: actualiza la UI al reiniciar
        if (textoTiempo != null)
        {
            textoTiempo.text = "0.00";
        }
    }

    public void TerminarJuego()
    {
        // Guarda el tiempo en GameData
        PlayerPrefs.SetInt("TiempoTotal", tiempoEnSegundos);

        // Carga la pantalla de puntuaci�n
        SceneManager.LoadScene("Score");
    }
}
