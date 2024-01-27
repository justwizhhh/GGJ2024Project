using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScribeManager : MonoBehaviour
{
    public static ScribeManager instance;

    public JokeSO fullJoke;
    public string currentLineOfJoke;
    public int blankAmount = 1;
    [SerializeField] private float jokeCoolDown;
    private Queue<int> blankNumbers = new Queue<int>();
    [Header("REQUIRED")]
    [SerializeField] private TextMeshProUGUI textmeshPro;
    [SerializeField] private BubbleManager bubbleManager;
    [SerializeField] private JokeManager jokeManager;

    private string[] splitWords;
    private bool lastOfJoke = false;
    private void Awake()
    {
        EventHandler.LoadJoke += GenerateJoke;
        EventHandler.WordTyped += OnWordTyped;
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //TESTING: REPLACE WITH CALL FROM JOKEMANAGER
        GenerateJoke();
        
    }

    void GenerateJoke() 
    {
        textmeshPro.color = Color.black;
        NewJoke(jokeManager.GetNextJoke());
    }

    void NewJoke(JokeSO joke) 
    {
        fullJoke = joke;
        currentLineOfJoke = fullJoke.jokeQuestion;
        GenerateBlanks();
        


        UpdateTMPRO();
    }

    public void UpdateTMPRO()
    {
        textmeshPro.text = OutputPrompt();
    }

    string OutputPrompt() 
    {
        return string.Join(" ", splitWords);
    }

    void GenerateBlanks() 
    {
        splitWords = currentLineOfJoke.Split(' ');
        string blankWords = "";
        List<int> unsortedBlanks = new List<int>();

        for (int i = 0; i < blankAmount; i++)
        {
            int blank = 0;
            if (i > splitWords.Length - 1) // Stops more then the phrases blanks being generated
                continue;

            for (int whileLoop = 0; whileLoop < 100; whileLoop++) 
            {
                blank = UnityEngine.Random.Range(0, splitWords.Length);
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
        //textmeshPro.text = OutputPrompt();

        if (blankNumbers.Count <= 0) 
        {
            OnJokeTyped();    
        }
    }

    void OnJokeTyped() 
    {
        if (BubbleManager.RemoveSpecialChars(currentLineOfJoke).ToUpper() == BubbleManager.RemoveSpecialChars(string.Join(' ',splitWords).ToUpper())) 
        {
            Debug.Log("SUCCESS!");
            textmeshPro.text = currentLineOfJoke;
            splitWords = currentLineOfJoke.Split(' ');
            EventHandler.OnCorrectAnswer();
            textmeshPro.color = Color.green;
            Invoke("FinishedLine", jokeCoolDown);

        }
        else 
        {
            Debug.Log("FAILURE");
            textmeshPro.color = Color.red;
            Invoke("GenerateJoke", jokeCoolDown);
            EventHandler.OnWrongAnswer();
        }
    }
    
    void FinishedLine() 
    {
        if (!lastOfJoke) 
        {
            //Question Finished
            lastOfJoke = true;
            currentLineOfJoke = fullJoke.jokePunchline;
            GenerateBlanks();

            textmeshPro.color = Color.black;
            textmeshPro.text = OutputPrompt();
        }
        else 
        {
            //Punchline Finished
            lastOfJoke = false;
            GenerateJoke();
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
