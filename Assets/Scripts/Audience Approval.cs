using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AudienceApproval : MonoBehaviour
{
    public Slider slider;
    public AnimationCurve positiveEffectCurve;
    public AnimationCurve negativeEffectCurve;
    public HealthBar healthUI;
    public float minInterval;
    public float maxInterval;
    public int activeThrowables;

    private void Start()
    {
        
    }

    public void SetActiveThrowables(int count)
    {
        activeThrowables = count;
    }

    public void SetMaxApproval(int approval)
    {
        slider.maxValue = approval;
        SetApproval(approval);
    }

    public void SetApproval(int approval)
    {
        healthUI.setHealth(approval);
    }

    private void Update()
    {
        
    }

    private void TypeWrong() 
    {
        
    }

    private void JokeCorrect() 
    {
        
    }

    public float GetPositiveEffect(float approvalRating)
    {
        return positiveEffectCurve.Evaluate(approvalRating / slider.maxValue);
    }

    public float GetNegativeEffect(float approvalRating)
    {
        return 1f - negativeEffectCurve.Evaluate(approvalRating / slider.maxValue);
    }
}