using Enumerations;
using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<int> ChangeHealth;
    public static event Action<int> ChangeRowdiness;
    public static event Action<int> ChangeScore;

    public static event Action<Vector2> Click;

    public static event Action<string> WordTyped;
    public static event Action<char> LetterTyped;

    public static event Action BeatHighScore;
    public static event Action FullJokeTold;
    public static event Action AnswerCorrect;
    public static event Action AnswerWrong;
    public static event Action TypeWrong;
    public static event Action LoadJoke;
    public static event Action PlayerDeath;

    public static event Action<GameState, bool> GameStateChanged;


    /// <summary>
    /// Messages all subscirbers when a word is typed WRONG
    /// </summary>
    /// <param name="amount"></param>
    public static void OnTypeWrong()
    {
        TypeWrong?.Invoke();
    }


    /// <summary>
    /// Messages all subscirbers when a full word has been typed in
    /// </summary>
    /// <param name="amount"></param>
    public static void OnTypeWord(string word)
    {
        WordTyped?.Invoke(word);
    }

    /// <summary>
    /// Messages all subscirbers when a full word has been typed in
    /// </summary>
    /// <param name="amount"></param>
    public static void OnBeatScore()
    {
        BeatHighScore?.Invoke();
    }


    /// <summary>
    /// Messages all subscirbers On Click
    /// </summary>
    public static void OnClick(Vector2 position)
    {
        Click?.Invoke(position);
    }





    /// <summary>
    /// Messages all Subs when the player dies.
    /// </summary>
    /// <param name="amount"></param>
    public static void OnDeath()
    {
        PlayerDeath?.Invoke();
    }



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
    /// Messages all subscirbers about the current letter being typed
    /// </summary>
    public static void OnLetterTyped(char letter)
    {
        LetterTyped?.Invoke(letter);
    }

    /// <summary>
    /// Messages all subscirbers about the player gave a correct answer
    /// </summary>
    public static void OnCorrectAnswer()
    {
        AnswerCorrect?.Invoke();
    }


    /// <summary>
    /// Messages all subscirbers when the player fully tells the joke
    /// </summary>
    public static void OnFullJoke()
    {
        AudioManager.instance.PlayOneShot(FMODLib.instance.answerRight);
        AudioManager.instance.PlayOneShot(FMODLib.instance.clapping);
        AudioManager.instance.PlayOneShot(FMODLib.instance.laughing);
        AudioManager.instance.PlayOneShot(FMODLib.instance.positivePhrase);
        AudioManager.instance.PlayOneShot(FMODLib.instance.woohoo);

        FullJokeTold?.Invoke();
    }

    /// <summary>
    /// Messages all subscirbers about the player gave a wrong answer
    /// </summary>
    public static void OnWrongAnswer()
    {
        AudioManager.instance.PlayOneShot(FMODLib.instance.answerWrong);
        AudioManager.instance.PlayOneShot(FMODLib.instance.booing);
        AudioManager.instance.PlayOneShot(FMODLib.instance.crickets);
        AudioManager.instance.PlayOneShot(FMODLib.instance.negativePhrase);
        AudioManager.instance.PlayOneShot(FMODLib.instance.whispering);
        AnswerWrong?.Invoke();
    }

    /// <summary>
    /// Messages all subscirbers about a new joke has been loaded
    /// </summary>
    public static void OnLoadJoke()
    {
        LoadJoke?.Invoke();
    }

    public static void OnSceneChange(GameState gameState, bool allowFadeOut = true)
    {
        GameStateChanged?.Invoke(gameState, allowFadeOut);
    }
}