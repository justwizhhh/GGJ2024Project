using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMOD;

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
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Player Health", slider.normalizedValue * 100.0f);
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("Player Health", out float result);
        UnityEngine.Debug.Log("Parameter value " + result);
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        healthText.SetText(health.ToString());
        
    }
}