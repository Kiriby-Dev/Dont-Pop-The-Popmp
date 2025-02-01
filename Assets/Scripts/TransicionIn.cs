using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TransicionIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel; // Arrastra aquí la imagen negra con CanvasGroup
    [SerializeField] private float fadeDuration = 1.5f; // Duración del fade
    [SerializeField] private float tiempoAntesFade;

    void Start()
    {
        StartCoroutine(StartSceneTransition());
    }

    IEnumerator StartSceneTransition()
    {
        yield return new WaitForSeconds(tiempoAntesFade);

        yield return StartCoroutine(Fade(0));

        //bubbleEffect.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(false);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadePanel.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = targetAlpha;
    }
}
