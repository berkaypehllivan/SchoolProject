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
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "SA�LIK : " + playerDamageable.Health;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        // Sa�l�k de�erini 0 ile maxHealth aras�nda s�n�rla
        float clampedHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        return clampedHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHeath)
    {
        // Sa�l�k de�erini 0 ile maxHeath aras�nda s�n�rla
        int clampedHealth = Mathf.Clamp(newHealth, 0, maxHeath);

        healthSlider.value = CalculateSliderPercentage(clampedHealth, maxHeath);
        healthBarText.text = "SA�LIK : " + clampedHealth;
    }
}
