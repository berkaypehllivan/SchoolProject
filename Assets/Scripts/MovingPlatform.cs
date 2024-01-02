using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PointA, PointB;
    public float speed;
    public float waitTime = 1f; // Bekleme s�resi (saniye cinsinden)
    Vector3 targetPoint;
    bool isWaiting = false;

    private void Start()
    {
        targetPoint = PointA.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            if (Vector2.Distance(transform.position, targetPoint) < 0.5f)
            {
                StartCoroutine(WaitAndChangeTarget());
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
    }

    IEnumerator WaitAndChangeTarget()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        if (targetPoint == PointA.position)
        {
            targetPoint = PointB.position;
        }
        else
        {
            targetPoint = PointA.position;
        }

        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // E�er platform aktif de�ilse, aktifle�tir
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            // Ebeveyn de�i�tirme i�lemini ger�ekle�tir
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ebeveyn de�i�tirme i�lemini ger�ekle�tir
            collision.transform.SetParent(null);
        }
    }
}
