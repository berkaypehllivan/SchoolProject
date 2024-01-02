using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable)
            {
                bool wasHealed = damageable.Heal(healthRestore);

                if (wasHealed && pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, transform.position, pickupSource.volume);
                    Destroy(gameObject);
                }
            }
        }
    }
}
