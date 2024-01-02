using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalEnemies;
    public ScoreScript scoreScript;
    private bool isLevelCompleted = false;

    public bool IsLevelCompleted()
    {
        return isLevelCompleted;
    }

    private void Start()
    {
        if (scoreScript == null)
        {
            scoreScript = FindObjectOfType<ScoreScript>();
        }
        scoreScript.SetTotalEnemies(totalEnemies);
    }

    // Her d��man�n �ld���nde �a�r�lacak fonksiyon
    public void EnemyDied()
    {
        scoreScript.OnEnemyDeath(); // ScoreScript'teki kalan d��man say�s�n� g�ncelle
        CheckLevelCompletion(); // B�l�m tamamland� m� diye kontrol et
    }

    public void CheckLevelCompletion()
    {
        if (scoreScript != null && scoreScript.GetRemainingEnemies() == 0)
        {
            isLevelCompleted = true;
            Debug.Log("B�t�n D��manlar �ld�r�ld�! Di�er B�l�me Ge�i� Yapabilirsiniz!");
        }
    }
}
