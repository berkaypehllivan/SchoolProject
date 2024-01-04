using System.Collections;
using UnityEngine;
using TMPro;


public class SlowText : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public float typingSpeed = 0.05f;

    private string fullTitle;
    private string fullDescription;

    private void Start()
    {
        fullTitle = titleText.text;
        fullDescription = descriptionText.text;
        titleText.text = "";
        descriptionText.text = "";
        StartCoroutine(TypeText(titleText, fullTitle, () =>
        {
            StartCoroutine(TypeText(descriptionText, fullDescription, null));
        }));
    }

    IEnumerator TypeText(TextMeshProUGUI textElement, string fullText, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        int currentCharacterIndex = 0;

        while (currentCharacterIndex < fullText.Length)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= typingSpeed)
            {
                textElement.text += fullText[currentCharacterIndex];
                currentCharacterIndex++;

                elapsedTime = 0f;
            }

            yield return null;
        }
        onComplete?.Invoke();
    }
}