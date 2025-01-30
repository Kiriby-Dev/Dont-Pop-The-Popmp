using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SceneStartTransition : MonoBehaviour
{
    public CanvasGroup fadePanel; // Arrastra aquí la imagen negra con CanvasGroup
    public ParticleSystem bubbleEffect; // Arrastra aquí el sistema de partículas
    public float fadeDuration = 1.5f; // Duración del fade
    public float bubbleDuration = 2f; // Tiempo que duran las burbujas
    public float bubbleFadeOutDuration = 2f;

    void Start()
    {
        StartCoroutine(StartSceneTransition());
    }

    IEnumerator StartSceneTransition()
    {
        // 1. Iniciar burbujas
        if (bubbleEffect != null)
        {
            bubbleEffect.Play();
        }

        // 2. Esperar antes del fade out (para que las burbujas sean visibles)
        yield return new WaitForSeconds(bubbleDuration);

        // 3. Fade Out (De negro a transparente)
        yield return StartCoroutine(Fade(0));

        yield return StartCoroutine(FadeOutBubbles());

        bubbleEffect.Stop();

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

    IEnumerator FadeOutBubbles()
    {
        if (bubbleEffect != null)
        {
            ParticleSystem.EmissionModule emission = bubbleEffect.emission;
            float startRate = emission.rateOverTime.constant;
            float elapsedTime = 0;

            while (elapsedTime < bubbleFadeOutDuration)
            {
                elapsedTime += Time.deltaTime;
                float newRate = Mathf.Lerp(startRate, 0, elapsedTime / bubbleFadeOutDuration);
                emission.rateOverTime = newRate;
                yield return null;
            }

            emission.rateOverTime = 0;
            bubbleEffect.Stop();
        }
    }
}
