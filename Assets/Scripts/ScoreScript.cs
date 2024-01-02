using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue;
    private int totalEnemies;
    public int remainingEnemies;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }



    public void SetTotalEnemies(int total)
    {
        totalEnemies = total;
        remainingEnemies = totalEnemies;
        UpdateScore();
    }

    public int GetRemainingEnemies()
    {
        return remainingEnemies;
    }

    public void UpdateScore()
    {
        scoreText.text = "Düþman Sayýsý: " + remainingEnemies.ToString() + " / " + totalEnemies.ToString();
    }

    public void OnEnemyDeath()
    {
        remainingEnemies--;

        if (remainingEnemies < 0)
        {
            remainingEnemies = 0;
        }

        UpdateScore();
    }

    public void SetScoreValue(int value)
    {
        scoreValue = value;
        UpdateScore();
    }
}