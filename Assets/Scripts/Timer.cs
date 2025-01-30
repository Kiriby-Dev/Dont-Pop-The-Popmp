using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tiempoEnSegundos = 0; // Contador de segundos
    private bool contar = true; // Bandera para detener/reanudar el contador
    private TMP_Text textoTiempo;  // Referencia a un objeto UI para mostrar el tiempo (opcional)
    bool click;

    private void Awake()
    {
        textoTiempo = GetComponent<TMP_Text>();
        click = false;
    }



    // Método principal del contador usando async/await
    void Update()
    {
        if (!click)
        {
            contar = false;
        }
        if (Input.GetMouseButton(0))
        {
            click = true;
            contar = true;
        }
            if (contar)
            {
                tiempoEnSegundos += Time.deltaTime;

                // Actualiza el texto de la UI si está configurado
                if (textoTiempo != null)
                {
                    textoTiempo.text = (tiempoEnSegundos).ToString("F2");
                }
            }
    }

    // Métodos para controlar el contador
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
        PlayerPrefs.SetFloat("TiempoTotal", tiempoEnSegundos);

        // Carga la pantalla de puntuación
        SceneManager.LoadScene("Score");
    }
}
