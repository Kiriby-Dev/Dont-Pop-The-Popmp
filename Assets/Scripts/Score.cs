using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header ("Configuracion")]
    [SerializeField] private TMP_Text textoTiempo;     // Texto para mostrar el tiempo total
    [SerializeField] private TMP_Text textoPuntuacion; // Texto para mostrar la puntuaci�n final
    [SerializeField] private TMP_Text textoNota;
    [SerializeField] private int puntuacionPorSegundo = 10; // Puntos por segundo de juego
    [SerializeField] private float velocidadIncremento = 0.01f; // Velocidad de incremento en segundos

    [Header("Notas")]
    [SerializeField] private int puntuacionS;
    [SerializeField] private int puntuacionA;
    [SerializeField] private int puntuacionB;
    [SerializeField] private int puntuacionC;
    [SerializeField] private int puntuacionD;
    [SerializeField] private int puntuacionE;

    private int puntuacionFinal; // Puntuaci�n total calculada
    private int tiempoTotal;

    private void Start()
    {
        tiempoTotal = PlayerPrefs.GetInt("TiempoTotal", 0);
        MostrarPantallaFinal();
    }

    // M�todo para mostrar la pantalla final
    private async void MostrarPantallaFinal()
    {
        // Muestra el tiempo total del jugador
        if (textoTiempo != null)
        {
            textoTiempo.text = (tiempoTotal/100f).ToString();
        }

        // Calcula la puntuaci�n final basada en el tiempo
        puntuacionFinal = (tiempoTotal * puntuacionPorSegundo)/100;

        // Inicia la animaci�n de puntuaci�n progresiva
        await MostrarPuntuacionProgresivamente();

        textoNota.text = NotaFinal(puntuacionFinal);
    }

    // M�todo para mostrar la puntuaci�n subiendo progresivamente
    private async Task MostrarPuntuacionProgresivamente()
    {
        int puntuacionActual = 0;

        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntuaci�n: 0"; // Inicializa el texto
        }

        while (puntuacionActual < puntuacionFinal)
        {
            // Incremento progresivo basado en un porcentaje del total
            puntuacionActual += 1;
            puntuacionActual = Mathf.Clamp(puntuacionActual, 0, puntuacionFinal);

            // Actualiza el texto en la UI
            if (textoPuntuacion != null)
            {
                textoPuntuacion.text = $"Puntuaci�n: {puntuacionActual}";
            }

            // Espera antes del siguiente incremento
            await Task.Delay((int)(velocidadIncremento * 325));
        }

        // Aseg�rate de que la puntuaci�n final se muestre correctamente
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"Puntuaci�n: {puntuacionFinal}";
        }
    }

    private string NotaFinal(int puntuacion) {
        if (puntuacion >= puntuacionS)
        {
            return "S";  // Excelente
        }
        else if (puntuacion >= puntuacionA)
        {
            return "A";  // Muy Bien
        }
        else if (puntuacion >= puntuacionB)
        {
            return "B";  // Muy Bien
        }
        else if (puntuacion >= puntuacionC)
        {
            return "C";  // Bien
        }
        else if (puntuacion >= puntuacionD)
        {
            return "D";  // Suficiente
        }
        else if (puntuacion >= puntuacionE)
        {
            return "E";  // Aprobado (en algunos sistemas podr�a ser suficiente para aprobar)
        }
        else
        {
            return "F";  // Reprobado
        }
    }
}
