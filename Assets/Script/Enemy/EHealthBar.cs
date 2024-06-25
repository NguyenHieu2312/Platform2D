using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EHealthBar : HealthBar
{
    public Image fillBar;
    public Image fillLater;
    public float heartSpeed = 0.005f;


    private void Start()
    {
        fillBar.fillAmount = 1f;
        fillLater.fillAmount = 1f;
    }
    private void Update()
    {
        if (fillLater.fillAmount > fillBar.fillAmount)
        {
            fillLater.fillAmount -= heartSpeed;
        }
        else
        {
            fillLater.fillAmount = fillBar.fillAmount;
        }
    }
    public override void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        fillBar.fillAmount = currentHealth / maxHealth;  
    }
}

