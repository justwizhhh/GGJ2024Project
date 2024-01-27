using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScribeManager : MonoBehaviour
{
    public string fullJoke;
    public int blankAmount = 1;
    private Queue<int> blankNumbers = new Queue<int>();
    [Header("REQUIRED")]
    [SerializeField] private TextMeshProUGUI textmeshPro;
    [SerializeField] private BubbleManager bubbleManager;

    private string[] splitWords;

    private void Awake()
    {
        EventHandler.WordTyped += OnWordTyped;
    }


    // Start is called before the first frame update
    void Start()
    {
        //TESTING: REPLACE WITH CALL FROM JOKEMANAGER
        NewJoke(fullJoke);
        
    }

    void NewJoke(string joke) 
    {
        fullJoke = joke;
        GenerateBlanks();


        textmeshPro.text = OutputPrompt();
    }

    string OutputPrompt() 
    {
        return string.Join(" ", splitWords);
    }

    void GenerateBlanks() 
    {
        splitWords = fullJoke.Split(' ');
        string blankWords = "";
        List<int> unsortedBlanks = new List<int>();

        for (int i = 0; i < blankAmount; i++)
        {
            int blank = 0;
            for (int whileLoop = 0; whileLoop < 100; i++) 
            {
                blank = UnityEngine.Random.Range(0, splitWords.Length - 1);
                if (!unsortedBlanks.Contains(blank)) // Check to stop duplicate blanks being created
                    break;
            }
             
            unsortedBlanks.Add(blank);
            blankWords += splitWords[blank];
            blankWords += " ";
            InsertBlank(blank);
        }

        unsortedBlanks.Sort();
        foreach (int sortedInt in unsortedBlanks) 
        {
            blankNumbers.Enqueue(sortedInt);
        }



        blankWords = blankWords.Remove(blankWords.Length - 1);
        bubbleManager.StartJoke(blankWords);
    }

    /*
    public string[] GetBlankWords() 
    {
        List<string> blankWords = new List<string>();
        foreach (int blankNumber in blankNumbers) 
        {
            blankWords.Add(splitWords[blankNumber]);
        }
        return blankWords.ToArray();
    } */

    void InsertBlank(int blank) 
    {
        splitWords[blank] = "_____";
    }


    void OnWordTyped(string word) 
    {
        splitWords[blankNumbers.Dequeue()] = word; //Replaces the blank space in order
        textmeshPro.text = OutputPrompt();

        if (blankNumbers.Count <= 0) 
        {
            OnJokeTyped();    
        }
    }

    void OnJokeTyped() 
    {
        if (BubbleManager.RemoveSpecialChars(fullJoke).ToUpper() == BubbleManager.RemoveSpecialChars(string.Join(' ',splitWords).ToUpper())) 
        {
            Debug.Log("SUCCESS!");
            textmeshPro.text = fullJoke;

        }
        else 
        {
            Debug.Log("FAILURE");
        }
    }
    
    static string CombineIntoOneSentence(string[] splitWords) 
    {
        string finalPhrase = "";
        foreach (string word in splitWords) 
        {
            finalPhrase += word;
        }
        return finalPhrase;
    }

}
