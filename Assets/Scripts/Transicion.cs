using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadePanel; // Imagen negra con CanvasGroup
    public ParticleSystem bubbleEffect; // Sistema de partículas

    public float fadeDuration = 1f; // Duración del fade
    public float transitionDelay = 2f; // Tiempo que dura la cortina de burbujas
    public float bubbleFadeOutDuration = 2f; // Tiempo en el que las burbujas disminuyen

    public void StartGame()
    {
        bubbleEffect.gameObject.SetActive(true);
        fadePanel.gameObject.SetActive(true);

        StartCoroutine(TransitionToLevel("Nivel1"));
    }

    IEnumerator TransitionToLevel(string sceneName)
    {
        // 1. Iniciar efecto de burbujas
        if (bubbleEffect != null)
        {
            bubbleEffect.Play();
        }

        // 2. Fade In (Oscurecer pantalla)
        yield return StartCoroutine(Fade(1));

        // 3. Esperar mientras las burbujas cubren la pantalla
        yield return new WaitForSeconds(transitionDelay);

        // 4. Iniciar la carga asíncrona de la escena
        yield return StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Evita que la escena se active automáticamente

        // Esperar a que la escena se cargue en segundo plano
        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // Espera un frame y sigue verificando
        }

        // Cuando la escena esté casi cargada, esperar un poco más si es necesario
        yield return new WaitForSeconds(0.5f);

        // Activar la escena cuando todo esté listo
        asyncLoad.allowSceneActivation = true;
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