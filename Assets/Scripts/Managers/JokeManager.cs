using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class JokeManager : MonoBehaviour
{
    [SerializeField]private TMP_Text jokeText;
    [SerializeField]private TMP_Text punchlineText;

    private List<JokeSO> listOfAllJokes = new();
    private Queue<JokeSO> queueOfJokes = new();

    private JokeSO currentJoke = null;

    private void Awake()
    {
        queueOfJokes = new();

        listOfAllJokes = LoadJokeObjects();
        ShuffleListOfAllJokes();
    }

    void Start()
    {
        currentJoke = GetNextJoke();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.N) && currentJoke != null)
        {
            currentJoke = GetNextJoke();
        }
    }

    private void ShuffleListOfAllJokes()
    {
        while(listOfAllJokes.Count > 0)
        {
            int randomIdx = (int)UnityEngine.Random.Range(0, listOfAllJokes.Count);
            queueOfJokes.Enqueue(listOfAllJokes[randomIdx]);
            listOfAllJokes.RemoveAt(randomIdx);
        }
    }

    private List<JokeSO> LoadJokeObjects()
    {
        return Resources.LoadAll<JokeSO>("Jokes").ToList();
    }

    private JokeSO GetNewJokesFromQueue()
    {
        return queueOfJokes.Dequeue();
    }

    public string[] GetQuestionAsArray()
    {
        return currentJoke.jokeQuestion.Split();
    }

    public string GetQuestionAsString()
    {
        return currentJoke.jokeQuestion;
    }
    public string[] GetPunchlineAsArray()
    {
        return currentJoke.jokePunchline.Split();
    }

    public string GetPunchlineAsString()
    {
        return currentJoke.jokePunchline;
    }

    public JokeSO GetNextJoke()
    {
        if (queueOfJokes.Count < 1)
        {
            jokeText.SetText("No more jokes for you...");
            punchlineText.SetText("No more punchlines either...");
            return null;
        }

        JokeSO joke = GetNewJokesFromQueue();
        jokeText.SetText(joke.jokeQuestion);
        punchlineText.SetText(joke.jokePunchline);

        EventHandler.OnLoadJoke();
        return joke;
    }
}
