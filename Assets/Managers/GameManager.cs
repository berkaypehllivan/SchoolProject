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

    // Her düþmanýn öldüðünde çaðrýlacak fonksiyon
    public void EnemyDied()
    {
        scoreScript.OnEnemyDeath(); // ScoreScript'teki kalan düþman sayýsýný güncelle
        CheckLevelCompletion(); // Bölüm tamamlandý mý diye kontrol et
    }

    public void CheckLevelCompletion()
    {
        if (scoreScript != null && scoreScript.GetRemainingEnemies() == 0)
        {
            isLevelCompleted = true;
            Debug.Log("Bütün Düþmanlar Öldürüldü! Diðer Bölüme Geçiþ Yapabilirsiniz!");
        }
    }
}
