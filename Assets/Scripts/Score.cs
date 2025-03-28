﻿using System;
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
    [SerializeField] private float tiempoPuntos = 3f;       // Tiempo total en el que debe sumarse el puntaje
    [SerializeField] private float tiempoPuntosBurbujas = 0.75f;

    [Header("Notas")]
    private int puntuacionS = 6000;
    private int puntuacionA = 4900;
    private int puntuacionB = 4100;
    private int puntuacionC = 3400;
    private int puntuacionD = 2300;
    private int puntuacionE = 800;

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
    private bool botonesHabilitados = false;

    private void Awake()
    {
        burbujas = new Image[] { burbuja1, burbuja2, burbuja3 };
    }

    private void Start()
    {
        tiempoTotal = PlayerPrefs.GetFloat("TiempoTotal", 0);
        StartCoroutine(MostrarPantallaFinal());
    }

    private IEnumerator MostrarPantallaFinal()
    {
        // Mostrar tiempo total en UI
        if (textoTiempo != null)
        {
            textoTiempo.text = "TIME: " + tiempoTotal.ToString("F2");
        }

        // Calcular la puntuación basada en el tiempo
        if (tiempoTotal < 148.295f)
        {
            puntuacionFinal = Mathf.FloorToInt((18759 - (3752.4f * Mathf.Log(tiempoTotal)))/10);
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

        audioPuntos.Pause();

        // Mostrar la nota final
        if (textoNota != null)
        {
            string notaFinal = NotaFinal(puntuacionFinal);
            textoNota.text = notaFinal;
            string nivel = PlayerPrefs.GetString("Nivel");
            int maxScore = PlayerPrefs.GetInt("Score" + nivel, 0);
            float bestTime = PlayerPrefs.GetFloat("Time" + nivel, 0);
            if (puntuacionFinal > maxScore) {
                PlayerPrefs.SetInt("Score" + nivel, puntuacionFinal);
                PlayerPrefs.SetString("Grade" + nivel, notaFinal);
            }

            if (tiempoTotal != 0 && bestTime == 0) {
                PlayerPrefs.SetFloat("Time" + nivel, tiempoTotal);
            }
            if (tiempoTotal < bestTime) {
                PlayerPrefs.SetFloat("Time" + nivel, tiempoTotal);
            }
            audioBurbuja.Play();
            StartCoroutine(AnimacionEscala(textoNota.transform));
            botonesHabilitados = true;
        }
    }

    private IEnumerator AgregarPuntosBurbujas(int cantBurbujas)
    {
        for (int i = 0; i < cantBurbujas; i++)
        {
            if (i < 2)
            {
                ChangeImage(burbujas[i], filledBubble);
            } 
            else 
            {
                ChangeImage(burbujas[0], goldenBubble);
                ChangeImage(burbujas[1], goldenBubble);
                ChangeImage(burbujas[2], goldenBubble);
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
            textoPuntuacion.text = $"SCORE: {puntuacionActual}";
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
                textoPuntuacion.text = $"SCORE: {puntuacionInterpolada}";
            }

            yield return null;
        }

        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"SCORE: {puntuacionFinal}";
        }
    }

    private void ChangeImage(Image target, Sprite sprite)
    {
        if (target != null)
        {
            if (audioBurbuja != null)
            {
                audioBurbuja.Play();
            }

            target.sprite = sprite;

            StartCoroutine(AnimacionEscala(target.transform));

        }
    }

    private IEnumerator AnimacionEscala(Transform target)
    {
        if (target == null) yield break; // Evita errores si el target es nulo

        Vector3 originalScale = target.localScale;
        Vector3 enlargedScale = originalScale * 1.2f;

        float duration = 0.2f;
        float elapsedTime = 0f;

        // Agrandar
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            target.localScale = Vector3.Lerp(originalScale, enlargedScale, progress);
            yield return null;
        }

        // Resetear el tiempo
        elapsedTime = 0f;

        // Reducir
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            target.localScale = Vector3.Lerp(enlargedScale, originalScale, progress);
            yield return null;
        }

        target.localScale = originalScale;
    }


    private string NotaFinal(int puntuacion)
    {
        int nivel = PlayerPrefs.GetInt("Nivel");
        Debug.Log("Calculando puntaje para el nivel "+ nivel);
        switch (nivel)
        {
            case 0:
                puntuacionS = 1330;
                puntuacionA = 1182;
                puntuacionB = 1034;
                puntuacionC = 738;
                puntuacionD = 433;
                puntuacionE = 147;
                break;
            case 1:
                puntuacionS = 900;
                puntuacionA = 800;
                puntuacionB = 700;
                puntuacionC = 500;
                puntuacionD = 300;
                puntuacionE = 100;
                break;
            case 2:
                puntuacionS = 900;
                puntuacionA = 800;
                puntuacionB = 700;
                puntuacionC = 500;
                puntuacionD = 300;
                puntuacionE = 100;
                break;
            case 3:
                puntuacionS = 925;
                puntuacionA = 822;
                puntuacionB = 720;
                puntuacionC = 513;
                puntuacionD = 308;
                puntuacionE = 102;
                break;
            case 4:
                puntuacionS = 970;
                puntuacionA = 862;
                puntuacionB = 754;
                puntuacionC = 538;
                puntuacionD = 323;
                puntuacionE = 107;
                break;
        }        
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
        if (botonesHabilitados) {
            PlayerPrefs.SetInt("Paused", 0);
            string lastLevel = PlayerPrefs.GetString("LastLevel", "Nivel1"); // Si no hay datos, vuelve a Nivel1
            SceneManager.LoadScene(lastLevel);
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("CantidadIntentos", 0);
        }
        
    }
 

    public void LoadMainMenu()
    {
        if (botonesHabilitados) {
            PlayerPrefs.SetInt("Paused", 0);
            Time.timeScale = 1f;    // Restaura el tiempo antes de cargar la escena
            GameObject prefMenu = GameObject.Find("UniquePrefab");
            Destroy(prefMenu);
            SceneManager.LoadScene("MenuNiveles");
        }
        
    }
}
