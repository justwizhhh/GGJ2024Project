using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private float basicTimer = 0;
    [SerializeField] private float tickTimer = 1;
    private void Start()
    {
        SetApproval(50);
        EventHandler.AnswerCorrect += JokeCorrect;
        EventHandler.AnswerWrong += JokeWrong;
        EventHandler.TypeWrong += TypeWrong;
    }

    private void Update()
    {
        if (basicTimer > tickTimer) 
        {
            AddApproval(-1);
            basicTimer = 0;
        }
        basicTimer += Time.deltaTime;
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

    public void AddApproval(int approval)
    {
        slider.value += approval;
        healthUI.setHealth();
    }

    public void SetApproval(int approval)
    {
        slider.value = approval;
        healthUI.setHealth();
    }

    private void TypeWrong() 
    {
        AddApproval(-5);
    }

    private void JokeCorrect() 
    {
        AddApproval(40);
    }

    private void JokeWrong()
    {
        AddApproval(-30);
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