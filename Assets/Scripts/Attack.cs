using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10; // Sald�r� hasar�
    public Vector2 knockback = Vector2.zero; // Geriye do�ru itme kuvveti
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localRotation.y >= 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            // E�er hasar verme i�lemi ba�ar�l�ysa, konsola log mesaj� yaz
            if (gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }
}

