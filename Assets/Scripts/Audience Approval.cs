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
    public float minInterval;
    public float maxInterval;
    public int activeThrowables;

    public void SetActiveThrowables(int count)
    {
        activeThrowables = count;
    }

    public void SetMaxApproval(int approval)
    {
        slider.maxValue = approval;
        slider.value = approval;
    }

    public void SetApproval(int approval)
    {
        slider.value = approval;
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