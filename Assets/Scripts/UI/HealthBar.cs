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
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        healthText.SetText(health.ToString());
        
    }
}