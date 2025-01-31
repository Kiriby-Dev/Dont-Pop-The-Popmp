using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private TMP_Text textoTiempo;     // Texto para mostrar el tiempo total
    [SerializeField] private TMP_Text textoPuntuacion; // Texto para mostrar la puntuación final
    [SerializeField] private TMP_Text textoNota;       // Texto para la nota final
    [SerializeField] private int puntuacionPorBurbuja; // Puntos por cada burbuja obtenida
    [SerializeField] private float tiempoPuntos;       // Tiempo total en el que debe sumarse el puntaje
    [SerializeField] private float tiempoPuntosBurbujas;

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
    [SerializeField] private Sprite goldenBubble;

    [Header("Audio")]
    [SerializeField] private AudioSource audioBurbuja;
    [SerializeField] private AudioSource audioPuntos;

    private int puntuacionFinal;
    private float tiempoTotal;

    private void Awake()
    {
        burbujas = new Image[] { burbuja1, burbuja2, burbuja3 };
    }

    private void Start()
    {
        // Datos de ejemplo (BORRAR en versión final)
        PlayerPrefs.SetFloat("TiempoTotal", 30f);
        PlayerPrefs.SetInt("CantidadBurbujas", 3);

        audioBurbuja = GetComponent<AudioSource>();
        audioPuntos = GetComponent<AudioSource>();
        tiempoTotal = PlayerPrefs.GetFloat("TiempoTotal", 0);
        StartCoroutine(MostrarPantallaFinal());
    }

    private IEnumerator MostrarPantallaFinal()
    {
        // Mostrar tiempo total en UI
        if (textoTiempo != null)
        {
            textoTiempo.text = "TIEMPO: " + tiempoTotal.ToString("F2");
        }

        // Calcular la puntuación basada en el tiempo
        if (tiempoTotal < 148.295f)
        {
            puntuacionFinal = Mathf.FloorToInt(18759 - (3752.4f * Mathf.Log(tiempoTotal)));
        }
        else
        {
            puntuacionFinal = 0;
        }

        audioPuntos.Play();

        // Mostrar el puntaje del tiempo
        yield return StartCoroutine(MostrarPuntuacionProgresivamente(0, tiempoPuntos));

        // Agregar puntos por burbujas con pausas entre cada una
        int cantBurbujas = PlayerPrefs.GetInt("CantidadBurbujas", 0);
        yield return StartCoroutine(AgregarPuntosBurbujas(cantBurbujas));

        audioBurbuja.Pause();

        // Mostrar la nota final
        if (textoNota != null)
        {
            textoNota.text = NotaFinal(puntuacionFinal);
        }
    }

    private IEnumerator AgregarPuntosBurbujas(int cantBurbujas)
    {
        for (int i = 0; i < cantBurbujas; i++)
        {
            if (i < 2)
            {
                StartCoroutine(ChangeImage(burbujas[i], filledBubble));
            } 
            else 
            {
                StartCoroutine(ChangeImage(burbujas[0], goldenBubble));
                StartCoroutine(ChangeImage(burbujas[1], goldenBubble));
                StartCoroutine(ChangeImage(burbujas[2], goldenBubble));
            }

            int puntuacionPrevia = puntuacionFinal;
            puntuacionFinal += puntuacionPorBurbuja;

            yield return StartCoroutine(MostrarPuntuacionProgresivamente(puntuacionPrevia, tiempoPuntosBurbujas));
        }
    }

    private IEnumerator MostrarPuntuacionProgresivamente(int puntuacionActual, float duracion)
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"PUNTAJE: {puntuacionActual}";
        }

        int puntuacionInicial = puntuacionActual;
        int diferenciaPuntos = puntuacionFinal - puntuacionInicial;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = Mathf.Clamp01(tiempoTranscurrido / duracion);

            int puntuacionInterpolada = puntuacionInicial + Mathf.RoundToInt(diferenciaPuntos * progreso);

            if (textoPuntuacion != null)
            {
                textoPuntuacion.text = $"PUNTAJE: {puntuacionInterpolada}";
            }

            yield return null;
        }

        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"PUNTAJE: {puntuacionFinal}";
        }
    }

    private IEnumerator ChangeImage(Image target, Sprite sprite)
    {
        if (target != null)
        {
            if (audioBurbuja != null)
            {
                audioBurbuja.Play();
            }

            target.sprite = sprite;

            // Guardamos el tamaño original
            Vector3 originalScale = target.transform.localScale;

            // Tamaño agrandado (10% más grande)
            Vector3 enlargedScale = originalScale * 1.2f;

            float duration = 0.2f; // Duración total de la animación
            float elapsedTime = 0f;

            // Animación de agrandamiento
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                target.transform.localScale = Vector3.Lerp(originalScale, enlargedScale, progress);
                yield return null;
            }

            // Resetear el tiempo
            elapsedTime = 0f;

            // Animación de reducción al tamaño original
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                target.transform.localScale = Vector3.Lerp(enlargedScale, originalScale, progress);
                yield return null;
            }

            // Asegurar que el tamaño final sea el original
            target.transform.localScale = originalScale;
        }
    }

    private string NotaFinal(int puntuacion)
    {
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
}
