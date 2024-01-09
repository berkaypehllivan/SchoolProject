using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public CanvasGroup InfoPanelCanvasGroup;
    public TextMeshProUGUI InfoText;

    [SerializeField] GameObject SwordAttack1, SwordAttack2, SwordAttack3;
    private Attack attack1, attack2, attack3;

    private void Start()
    {
        // SwordAttack1, SwordAttack2 ve SwordAttack3 objelerinden Attack bileþenlerini al
        attack1 = SwordAttack1.GetComponent<Attack>();
        attack2 = SwordAttack2.GetComponent<Attack>();
        attack3 = SwordAttack3.GetComponent<Attack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int damageIncrease = 5;
            UpdateAttackValues(attack1, damageIncrease);
            UpdateAttackValues(attack2, damageIncrease);
            UpdateAttackValues(attack3, damageIncrease);
            StartCoroutine(ShowInfoPanel());
        }
    }

    private void UpdateAttackValues(Attack attack, int damageIncrease)
    {
        // Eðer Attack bileþeni varsa ve attackDamage deðeri üzerine bir artýþ eklemek istiyorsak
        if (attack != null)
        {
            attack.attackDamage += damageIncrease;
            // Diðer özellikleri de güncellemek istiyorsanýz buraya ekleyebilirsiniz
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
        yield return TypeText("Karakterin Güçlendi.", 0.1f);
        yield return new WaitForSeconds(0.5f);
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
