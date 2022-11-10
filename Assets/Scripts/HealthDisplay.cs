using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Gradient gradient;

    public DamageHandler damageHandler;

    float healthPercentage;

    Slider slider;

    public Image fill;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        healthPercentage = damageHandler.health / damageHandler.maxHealth;
        slider.value = Mathf.Lerp(slider.value, healthPercentage, Time.deltaTime);
        fill.color = gradient.Evaluate(healthPercentage);
    }
}
