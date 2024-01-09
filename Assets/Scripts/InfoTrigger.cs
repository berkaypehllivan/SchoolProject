using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoTrigger : MonoBehaviour
{

    public CanvasGroup InfoPanelCanvasGroup;
    public TextMeshProUGUI InfoText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ShowInfoPanel());
        }
    }


    private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, float fadeDuration)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
    private IEnumerator ShowInfoPanel()
    {
        InfoText.text = "";
        InfoPanelCanvasGroup.gameObject.SetActive(true);
        InfoPanelCanvasGroup.alpha = 1f;
        yield return TypeText("Yukarýya çýkmanýn bir yolu olmalý.  (Duvarlara Týrmanmak Ýçin 'W' Tuþunu Kullan)", 0.06f);
        yield return new WaitForSeconds(1f);
        yield return FadeOutCanvasGroup(InfoPanelCanvasGroup, 1.5f);
        InfoPanelCanvasGroup.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private IEnumerator TypeText(string text, float typingSpeed)
    {
        foreach (char letter in text)
        {
            InfoText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
