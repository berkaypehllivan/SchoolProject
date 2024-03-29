using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScene : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadeImage; // veya Text

    private void Start()
    {
        // Ba�lang��ta FadeImage'i g�r�n�r yap ve FadeIn i�lemini ba�lat
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    public void Restart()
    {
        StartCoroutine(FadeOutAndLoadScene());
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

        // Fade-in tamamland���nda, FadeImage'i deaktive et
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeOutAndLoadScene()
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

        // GameOver sahnesinden tekrar deneye bast���m�zda bu metot �al��acak
        LoadSavedLevel();
    }

    private void LoadSavedLevel()
    {
        // PlayerPrefs'ten kaydedilmi� level bilgisini al
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1); // Varsay�lan olarak level-1

        // Kaydedilen level'i y�kle
        SceneManager.LoadScene(savedLevel);
    }
}