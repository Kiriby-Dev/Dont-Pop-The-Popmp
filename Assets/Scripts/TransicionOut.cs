using UnityEngine;
using System.Collections;

public class TransicionOut : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public ParticleSystem bubbleEffect;
    public float fadeDuration = 1.5f;
    public float bubbleDuration = 2f;
    public float bubbleFadeOutDuration = 2f;

    private string transitionKey;

    void Start()
    {
        transitionKey = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (PlayerPrefs.GetInt(transitionKey, 0) == 0) // Si no se ha ejecutado antes
        {
            PlayerPrefs.SetInt(transitionKey, 1);
            StartCoroutine(StartSceneTransition());
        }
        else
        {
            fadePanel.gameObject.SetActive(false);
            bubbleEffect.gameObject.SetActive(false);
        }
    }

    IEnumerator StartSceneTransition()
    {
        if (bubbleEffect != null) bubbleEffect.Play();

        yield return new WaitForSeconds(bubbleDuration);

        yield return StartCoroutine(Fade(0));
        yield return StartCoroutine(FadeMusica(PlayerPrefs.GetFloat("valorAudio", 1f)));

        yield return StartCoroutine(FadeOutBubbles());

        bubbleEffect.Stop();
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
    IEnumerator FadeMusica(float targetVolume)
    {
        float startVolume = 0;
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
