using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int trapDamage = 100; // Trap hasar�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(trapDamage, Vector2.zero); // Geriye do�ru itme kuvveti s�f�r

            // E�er hasar verme i�lemi ba�ar�l�ysa, konsola log mesaj� yaz
            if (gotHit)
                Debug.Log(collision.name + " hit for " + trapDamage + " damage");
        }
    }
}