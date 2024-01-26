using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JokeManager : MonoBehaviour
{
    private TMP_Text jokeText;
    private Queue<JokeSO> listOfJokes;

    private void Awake()
    {
        jokeText = GetComponent<TMP_Text>();
        listOfJokes = CreateQueueOfJokes();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Queue<JokeSO> CreateQueueOfJokes()
    {
        // Shuffle the jokes in the jokes database and arrange them into a queue, then return the queue
        return new Queue<JokeSO>(); // placeholder
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
