using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Door : MonoBehaviour
{
    public CanvasGroup InfoPanelCanvasGroup;
    public TextMeshProUGUI InfoText;
    public GameObject player;
    public Sprite openDoorImage;
    public Sprite closeDoorImage;
    public float timeBeforeNextScene;
    private GameManager gameManager;
    private ScoreScript scoreScript;
    private AudioSource doorAudioSource;
    FadeInOut fade;

    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;

    private bool playerIsAtTheDoor;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreScript = FindAnyObjectByType<ScoreScript>();
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        fade = FindAnyObjectByType<FadeInOut>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsAtTheDoor)
        {
            if (gameManager.IsLevelCompleted())
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                StartCoroutine(ShowInfoPanel());
                Debug.Log("Bütün Düþmanlarý Öldürmen Gerekiyor! Kalan Düþman Sayýsý: " + scoreScript.remainingEnemies);
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorImage;
        yield return new WaitForSeconds(timeBeforeNextScene);
        GetComponent<SpriteRenderer>().sprite = closeDoorImage;
        doorAudioSource.PlayOneShot(openDoorSound);
        yield return new WaitForSeconds(timeBeforeNextScene);
        player.SetActive(false);
        fade.FadeIn();
        yield return new WaitForSeconds(timeBeforeNextScene);
        SceneManager.LoadScene("Level-2");
    }

    private IEnumerator ShowInfoPanel()
    {
        InfoText.text = "";
        InfoPanelCanvasGroup.gameObject.SetActive(true);
        InfoPanelCanvasGroup.alpha = 1f;
        yield return TypeText("Bütün Düþmanlarý Öldürmen Gerekiyor! Kalan Düþman Sayýsý: " + scoreScript.remainingEnemies, 0.05f);
        yield return new WaitForSeconds(1f);
        yield return FadeOutCanvasGroup(InfoPanelCanvasGroup, 1.5f);
        InfoPanelCanvasGroup.gameObject.SetActive(false);
    }

    private IEnumerator TypeText(string text, float typingSpeed)
    {
        foreach (char letter in text)
        {
            InfoText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsAtTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsAtTheDoor = false;
        }
    }
}