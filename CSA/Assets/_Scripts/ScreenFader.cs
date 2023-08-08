using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : Singleton<ScreenFader>
{
    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1.0f;

    public static bool isFading { get; internal set; }

    private void Awake()
    {
        GameManager.screennFader = this;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            faderCanvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        faderCanvasGroup.alpha = 0f;
        isFading = false;
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            faderCanvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        faderCanvasGroup.alpha = 1f;
        isFading = false;
    }
}
