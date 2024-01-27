using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class JokeManager : MonoBehaviour
{
    private TMP_Text jokeText;
    private List<JokeSO> listOfAllJokes;
    private Queue<JokeSO> queueOfJokes;

    private void Awake()
    {
        jokeText = GetComponent<TMP_Text>();
        queueOfJokes = new();

        listOfAllJokes = LoadJokeObjects();
        ShuffleListOfAllJokes();
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

    public JokeSO GetNewJokesFromQueue()
    {
        return queueOfJokes.Dequeue();
    }

    public string[] GetJokeAsSplitText()
    {
        return jokeText.text.Split();
    }

    public string GetJokeAsString()
    {
        return jokeText.text;
    }

    public bool LoadNextJoke()
    {
        // Logic to load the next joke
        EventHandler.OnLoadJoke();
        return false;
    }
}
