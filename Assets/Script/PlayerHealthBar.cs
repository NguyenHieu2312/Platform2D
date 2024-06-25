using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PLayerHealthBar : HealthBar
{
    public Image fillWhenTakeDamageBar;
    public Image fillLater;
    public float minusHelthSpeed;
    public TextMeshProUGUI _healthToText;

    private void Update()
    {
        if (fillLater.fillAmount > fillWhenTakeDamageBar.fillAmount)
        {
            fillLater.fillAmount -= minusHelthSpeed;
        }
        else
        {
            fillLater.fillAmount = fillWhenTakeDamageBar.fillAmount;
        }
    }
    public override void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        fillWhenTakeDamageBar.fillAmount = currentHealth / maxHealth;
        _healthToText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
