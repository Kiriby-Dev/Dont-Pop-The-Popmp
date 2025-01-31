using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private TMP_Text textoTiempo;     // Texto para mostrar el tiempo total
    [SerializeField] private TMP_Text textoPuntuacion; // Texto para mostrar la puntuación final
    [SerializeField] private TMP_Text textoNota; // Puntos por segundo de juego
    [SerializeField] private int puntuacionPorBurbuja; // Puntos por burbuja conseguida
    [SerializeField] private float velocidadIncremento; // Velocidad de incremento en segundos
    [SerializeField] private float tiempoPuntos;

    [Header("Notas")]
    [SerializeField] private int puntuacionS;
    [SerializeField] private int puntuacionA;
    [SerializeField] private int puntuacionB;
    [SerializeField] private int puntuacionC;
    [SerializeField] private int puntuacionD;
    [SerializeField] private int puntuacionE;

    [Header("Burbujas")]
    [SerializeField] private Image burbuja1;
    [SerializeField] private Image burbuja2;
    [SerializeField] private Image burbuja3;
    private Image[] burbujas;
    [SerializeField] private Sprite filledBubble;

    private SpriteRenderer spriteRenderer;

    private int puntuacionFinal; // Puntuación total calculada
    private float tiempoTotal;

    private void Awake()
    {
        burbujas = new Image[] { burbuja1, burbuja2, burbuja3 };
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tiempoTotal = PlayerPrefs.GetFloat("TiempoTotal", 0);
        MostrarPantallaFinal();
    }

    // Método para mostrar la pantalla final
    private async void MostrarPantallaFinal()
    {
        // Muestra el tiempo total del jugador
        if (textoTiempo != null)
        {
            textoTiempo.text = "TIEMPO: " + (tiempoTotal).ToString("F2");
        }

        if (tiempoTotal < 148.295f)
        {
            puntuacionFinal = Mathf.FloorToInt(18759 - (3752.4f*MathF.Log(tiempoTotal)));
        }
        else
        {
            puntuacionFinal = 0;
        }

        // Inicia la animación de puntuación progresiva
        await MostrarPuntuacionProgresivamente(0);

        int cantBurbujas = PlayerPrefs.GetInt("CantidadBurbujas", 0);

        await AgregarPuntosBurbujas(cantBurbujas);

        textoNota.text = NotaFinal(puntuacionFinal);
    }

    private async Task AgregarPuntosBurbujas(int cantBurbujas)
    {
        for (int i = 0; i < cantBurbujas; i++)
        {
            await Task.Delay(500);
            ChangeImage(burbujas[i]);
            int puntuacionPrevia = puntuacionFinal;
            puntuacionFinal += puntuacionPorBurbuja;
            await MostrarPuntuacionProgresivamente(puntuacionPrevia);
            
        }
    }

    public void ChangeImage(Image target)
    {
        //Image imageComponent = target.GetComponent<Image>();

        if (target != null)
        {
            target.sprite = filledBubble;
        }
    }

    // Método para mostrar la puntuación subiendo progresivamente
    private async Task MostrarPuntuacionProgresivamente(int puntuacionActual)
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "PUNTAJE: 0"; // Inicializa el texto
        }

        // Calcular la cantidad de incrementos necesarios
        int totalIncrementos = puntuacionFinal - puntuacionActual;

        // Asegúrate de que totalIncrementos sea mayor que 0
        if (totalIncrementos <= 0)
            return;

        // Calcular el tiempo de espera para cada incremento (total de 5 segundos)
        float tiempoPorIncremento = tiempoPuntos / totalIncrementos;  // 5 segundos / cantidad de incrementos

        while (puntuacionActual < puntuacionFinal)
        {
            // Incremento progresivo
            puntuacionActual += 1;
            puntuacionActual = Mathf.Clamp(puntuacionActual, 0, puntuacionFinal);

            // Actualiza el texto en la UI
            if (textoPuntuacion != null)
            {
                textoPuntuacion.text = $"PUNTAJE: {puntuacionActual}";
            }

            // Espera antes del siguiente incremento
            await Task.Delay((int)(tiempoPorIncremento * 1000));  // Convertir a milisegundos
        }

        // Asegúrate de que la puntuación final se muestre correctamente
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"PUNTAJE: {puntuacionFinal}";
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
            return "E";  // Aprobado (en algunos sistemas podría ser suficiente para aprobar)
        }
        else
        {
            return "F";  // Reprobado
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Paused", 0);
        SceneManager.LoadScene("Nivel1");
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CantidadIntentos", 0);
        PlayerPrefs.Save();
    }

    public void LoadMenuInicial()
    {
       SceneManager.LoadScene("MenuInicio");
    }
}
