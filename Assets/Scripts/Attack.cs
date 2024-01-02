using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10; // Saldýrý hasarý
    public Vector2 knockback = Vector2.zero; // Geriye doðru itme kuvveti
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localRotation.y >= 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            // Eðer hasar verme iþlemi baþarýlýysa, konsola log mesajý yaz
            if (gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }
}

