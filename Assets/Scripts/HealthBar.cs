using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    private void Start()
    {
        // Baþlangýçta healthSlider.value'yi doðru bir þekilde ayarla
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        // Baþlangýçta healthBarText'i doðru bir þekilde ayarla
        healthBarText.text = "SAÐLIK: " + playerDamageable.Health;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth,float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth,int maxHeath)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHeath);
        healthBarText.text = "SAÐLIK: " + newHealth;
    }
}
