using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CompleteScript : MonoBehaviour
{
    public CanvasGroup canvas;
    public float timeToFadeIn = 1f;
    public float timeToFadeOut = 1f;
    [SerializeField] private float delayBeforeTransition;

    private void Start()
    {
        StartCoroutine(CompleteSequence());
    }

    IEnumerator CompleteSequence()
    {
        FadeOut();
        yield return new WaitForSeconds(timeToFadeOut + delayBeforeTransition);
        FadeIn();
        yield return new WaitForSeconds(timeToFadeIn);
        yield return new WaitForSeconds(1f);
        string nextSceneName = "Start";
        SceneManager.LoadScene(nextSceneName);
    }

    private void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvas, 0f, 1f, timeToFadeIn));
    }

    private void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvas, 1f, 0f, timeToFadeOut));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
