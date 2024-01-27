using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthText;

    public void setHealth()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.SetText(slider.value.ToString());
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        healthText.SetText(health.ToString());
        
    }
}