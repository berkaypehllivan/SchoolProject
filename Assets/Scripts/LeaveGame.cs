using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveGame : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadeImage;

    public void QuitGame()
    {
        StartCoroutine(FadeOutAndQuit());
    }

    IEnumerator FadeOutAndQuit()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color c = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        Application.Quit();
    }
}
