using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using FMODUnity;

public class JokeManager : MonoBehaviour
{
    [SerializeField]private TMP_Text jokeText;
    [SerializeField]private TMP_Text punchlineText;
    [SerializeField] private EventReference TestSound;

    private List<JokeSO> listOfAllJokes = new();
    private Queue<JokeSO> queueOfJokes = new();

    private JokeSO currentJoke = null;

    private void Awake()
    {
        listOfAllJokes = LoadJokeObjects();
        ShuffleListOfAllJokes();
    }

    void Start()
    {
        currentJoke = GetNextJoke();
    }

    void Update()
    {
        // TEST CODE... TO BE REMOVED
        if(Input.GetKeyUp(KeyCode.N) && currentJoke != null)
        {
            EventHandler.OnPlayOneShotAudio(TestSound);
            currentJoke = GetNextJoke();
        }
    }

    /// <summary>
    /// Shuffles all the jokes from the list of all jokes and queues them
    /// </summary>
    private void ShuffleListOfAllJokes()
    {
        while(listOfAllJokes.Count > 0)
        {
            int randomIdx = (int)UnityEngine.Random.Range(0, listOfAllJokes.Count);
            queueOfJokes.Enqueue(listOfAllJokes[randomIdx]);
            listOfAllJokes.RemoveAt(randomIdx);
        }
    }

    /// <summary>
    /// Loads all the JokeSO objects from the Assets/Resources/Jokes folder
    /// </summary>
    /// <returns>List of JokeSO</returns>
    private List<JokeSO> LoadJokeObjects()
    {
        return Resources.LoadAll<JokeSO>("Jokes").ToList();
    }

    /// <summary>
    /// Dequeues the next joke from the queue
    /// </summary>
    /// <returns></returns>
    private JokeSO GetNewJokesFromQueue()
    {
        return queueOfJokes.Dequeue();
    }

    /// <summary>
    /// Gets the question of the current joke as a string array
    /// </summary>
    /// <returns>string[]</returns>
    public string[] GetQuestionAsArray()
    {
        return currentJoke.jokeQuestion.Split();
    }

    /// <summary>
    /// Gets the question of the current joke as a string
    /// </summary>
    /// <returns>string</returns>
    public string GetQuestionAsString()
    {
        return currentJoke.jokeQuestion;
    }

    /// <summary>
    /// Gets the punchline of the current joke as a string array
    /// </summary>
    /// <returns>string[]</returns>
    public string[] GetPunchlineAsArray()
    {
        return currentJoke.jokePunchline.Split();
    }

    /// <summary>
    /// Gets the punchline of the current joke as a string
    /// </summary>
    /// <returns>string</returns>
    public string GetPunchlineAsString()
    {
        return currentJoke.jokePunchline;
    }

    /// <summary>
    /// Loads the next joke from the queue
    /// </summary>
    /// <returns>JokeSO - The joke scriptable object</returns>
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

        //EventHandler.OnLoadJoke();
        return joke;
    }
}
