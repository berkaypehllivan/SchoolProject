using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int trapDamage = 100; // Trap hasarý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(trapDamage, Vector2.zero); // Geriye doðru itme kuvveti sýfýr

            // Eðer hasar verme iþlemi baþarýlýysa, konsola log mesajý yaz
            if (gotHit)
                Debug.Log(collision.name + " hit for " + trapDamage + " damage");
        }
    }
}