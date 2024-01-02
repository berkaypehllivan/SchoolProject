using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public GameObject player;
    public Sprite openDoorImage;
    public Sprite closeDoorImage;
    public float timeBeforeNextScene;
    public bool playerIsAtTheDoor;
    private GameManager gameManager;
    private ScoreScript scoreScript;
    private AudioSource doorAudioSource;
    FadeInOut fade;

    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreScript = FindAnyObjectByType<ScoreScript>();
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        fade = FindAnyObjectByType<FadeInOut>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsAtTheDoor && gameManager.IsLevelCompleted())
        {
            StartCoroutine(OpenDoor());
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerIsAtTheDoor && !gameManager.IsLevelCompleted())
        {
            Debug.Log("Bütün Düþmanlarý Öldürmen Gerekiyor! Kalan Düþman Sayýsý: " + scoreScript.remainingEnemies);
        }
    }

    public IEnumerator OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorImage;
        yield return new WaitForSeconds(timeBeforeNextScene);
        doorAudioSource.PlayOneShot(openDoorSound);
        player.SetActive(false);
        yield return new WaitForSeconds(timeBeforeNextScene);
        GetComponent<SpriteRenderer>().sprite = closeDoorImage;
        fade.FadeIn();
        yield return new WaitForSeconds(timeBeforeNextScene);
        SceneManager.LoadScene("Level-2");
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

