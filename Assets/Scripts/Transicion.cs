using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel; // Imagen negra con CanvasGroup

    [SerializeField] private float fadeDuration = 1f; // Duraci�n del fade
    private AsyncOperation sceneLoadOperation;

    public void StartGame(string escena)
    {
        sceneLoadOperation = SceneManager.LoadSceneAsync(escena);
        sceneLoadOperation.allowSceneActivation = false; // No activar a�n

        fadePanel.gameObject.SetActive(true);

        StartCoroutine(TransitionToLevel());
    }

    IEnumerator TransitionToLevel()
    {

        // Fade out de la m�sica y pantalla
        yield return StartCoroutine(Fade(1));
        yield return StartCoroutine(FadeMusica(0));

        // Activar la escena precargada sin retrasos
        sceneLoadOperation.allowSceneActivation = true;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Evita que la escena se active autom�ticamente

        // Esperar a que la escena se cargue en segundo plano
        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // Espera un frame y sigue verificando
        }

        // Cuando la escena est� casi cargada, esperar un poco m�s si es necesario
        yield return new WaitForSeconds(0.5f);

        // Activar la escena cuando todo est� listo
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

    IEnumerator FadeMusica(float targetVolume)
    {
        float startVolume = PlayerPrefs.GetFloat("valorAudio", 1f);
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            AudioListener.volume = volume;
            yield return null;
        }
    }
}