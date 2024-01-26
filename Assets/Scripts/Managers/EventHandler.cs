using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<int> ChangeHealth;
    public static event Action<int> ChangeRowdiness;
    public static event Action<int> ChangeScore;

    public static event Action AnswerCorrect;
    public static event Action AnswerWrong;
    public static event Action LoadJoke;

    /// <summary>
    /// Messages all subscirbers about the change in the player's health value
    /// </summary>
    /// <param name="amount"></param>
    public static void OnChangeHealth(int amount)
    {
        ChangeHealth?.Invoke(amount);
    }

    /// <summary>
    /// Messages all subscirbers about the change in the rowdiness value
    /// </summary>
    /// <param name="amount"></param>
    public static void OnChangeRowdiness(int amount)
    {
        ChangeRowdiness?.Invoke(amount);
    }

    /// <summary>
    /// Messages all subscirbers about the change in the player's score value
    /// </summary>
    /// <param name="amount"></param>
    public static void OnChangeScore(int amount)
    {
        ChangeScore?.Invoke(amount);
    }

    /// <summary>
    /// Messages all subscirbers about the player gave a correct answer
    /// </summary>
    public static void OnCorrectAnswer()
    {
        AnswerCorrect?.Invoke();
    }

    /// <summary>
    /// Messages all subscirbers about the player gave a wrong answer
    /// </summary>
    public static void OnWrongAnswer()
    {
        AnswerWrong?.Invoke();
    }

    /// <summary>
    /// Messages all subscirbers about a new joke has been loaded
    /// </summary>
    public static void OnLoadJoke()
    {
        LoadJoke?.Invoke();
    }
}
