using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadeImage; // veya Text

    private void Start()
    {
        // Baþlangýçta FadeImage'i görünür yap ve FadeIn iþlemini baþlat
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    public void StartGame()
    {
        StartCoroutine(FadeOutAndLoadScene("Level-1"));
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color c = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        // Fade-in tamamlandýðýnda, FadeImage'i deaktive et
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
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
        SceneManager.LoadScene("Level-1");
    }
}
