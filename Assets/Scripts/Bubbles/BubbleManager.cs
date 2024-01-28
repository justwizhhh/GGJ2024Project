using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.UIElements;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject poppedBubble;
    [SerializeField] private Transform[] audiencePositions;
    [SerializeField] private Transform poppedBubbleTarget;

    [SerializeField] private string fakeJokes;
    [SerializeField] private float floatingSpeed = 1;
    [SerializeField] private float maxheight;
    [SerializeField] private float minheight;

    [SerializeField] private bool debugLog = false;


    [HideInInspector] public List<SpawnedBubble> bubbleRoyale = new List<SpawnedBubble>();
    [HideInInspector] public List<SpawnedBubble> bubbleList = new List<SpawnedBubble>();
    private int characterNum = 0;

    private void Awake()
    {
        EventHandler.LetterTyped += OnType;
        EventHandler.FullJokeTold += DeleteBubbles;
        EventHandler.PlayerDeath += OnPlayerDeath;
    }


    void OnPlayerDeath() 
    {
        DeleteBubbles();

        this.enabled = false;
    }

    void DeleteBubbles() 
    {
        foreach (SpawnedBubble bubbles in bubbleList) 
        {
            GameObject.Destroy(bubbles.gameObject);
        }
        bubbleList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpawnedBubble bubble in bubbleList) 
        {
            if (bubble.transform.position.y > maxheight) 
            {
                bubble.transform.position = new Vector3(bubble.transform.position.x, minheight);
            }
           
            else if (bubble.transform.position.y < minheight) 
            {
                bubble.rigidBody.MovePosition(new Vector2(bubble.transform.position.x, minheight));
                bubble.rigidBody.gravityScale = -0.03f * floatingSpeed * 3;
                bubble.rigidBody.velocity = new Vector2(bubble.rigidBody.velocity.x, 2.7f);
            }
            else 
            {
                bubble.rigidBody.gravityScale = -0.03f * floatingSpeed;
            }
        }
    }



    void OnType(char input) 
    {
        SpawnedBubble[] bubbleRound = bubbleRoyale.ToArray();
        foreach (SpawnedBubble aliveBubble in bubbleRound)
        {
            if (aliveBubble.text.Length - 2 < characterNum && aliveBubble.text.ToUpper()[characterNum] == input) // Check if the final character of a bubble
            {
                //Picked option
                if (debugLog) Debug.Log($"Picked Text Bubble: {aliveBubble.text}, {aliveBubble.gameObject.name}");
                EventHandler.OnTypeWord(aliveBubble.text);
                ResetSelection();
                FinishWord(aliveBubble);
                return;
            }

            if (aliveBubble.text.ToUpper()[characterNum] != input) 
            {
                bubbleRoyale.Remove(aliveBubble);
                aliveBubble.TMPRO.text = aliveBubble.text;
            }
            else 
            {
                aliveBubble.TMPRO.text = BoldenText(aliveBubble.text, characterNum);
            }
        }


        if (bubbleRoyale.Count <= 0)
        {
            // No more options left 
            // PUNISH THE PLAYER and reset.
            if (debugLog) Debug.Log("TYPING FAILURE! PUNISH!");
            EventHandler.OnTypeWrong();
            ResetSelection();
        }
        else
        {
            characterNum++;
        }
    }

    void ResetSelection() 
    {
        characterNum = 0;

        bubbleRoyale.Clear();
        bubbleRoyale.AddRange(bubbleList);

        foreach (SpawnedBubble bubble in bubbleList) 
        {
            bubble.TMPRO.text = bubble.text;
        }

    }

    static string BoldenText(string text, int boldAmount) 
    {
        string newText = "<b>";
        for (int i = 0; i <= boldAmount; i++) 
        {
            newText += text[i];
        }
        newText += "</b>";

        for (int i = boldAmount + 1; i < text.Length; i++)
        {
            newText += text[i];
        }

        return newText;
    }


    [ContextMenu("Test Spawn")]
    void TestSpawn() 
    {
        StartJoke("Why did the chicken cross the road?");
    }

    void FinishWord(SpawnedBubble finishedBubble)
    {
        bubbleList.Remove(finishedBubble);

        BubbleAnimator whatsPopping = GameObject.Instantiate(poppedBubble, finishedBubble.transform.position, finishedBubble.transform.rotation).GetComponent<BubbleAnimator>();
        whatsPopping.Target = poppedBubbleTarget;
        whatsPopping.text.text = finishedBubble.text;

        Destroy(finishedBubble.gameObject);
    }


    void SpawnWord(string word) 
    {
        Vector3 spawnPos = audiencePositions[UnityEngine.Random.Range(0, audiencePositions.Length - 1)].transform.position;
        Quaternion randomRotation = Quaternion.Euler(0,0,UnityEngine.Random.Range(0,360));
        SpawnedBubble newBubble = GameObject.Instantiate(bubble, spawnPos, randomRotation, this.transform).GetComponent<SpawnedBubble>();


        //Intalise Bubble
        newBubble.TMPRO.text = word;
        newBubble.text = word;
        newBubble.rigidBody.velocity = new Vector2(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(0.0f, 2.0f));
        newBubble.rigidBody.gravityScale = -0.03f * floatingSpeed;

        // Get width of the TMPRO
        
        bubbleList.Add(newBubble);
    }

    public static string RemoveSpecialChars(string word) 
    {
        string newWord = "";
        for (int i = 0; i < word.Length; i++) 
        {
            if (Char.IsLetter(word[i])) 
            {
                newWord += word[i];
            }
        }

        return newWord;
    }
    
    public void StartJoke(string Joke) 
    {
        string[] words = Joke.Split(' ');
        
        foreach (string word in words) 
        {
            string sorted = RemoveSpecialChars(word); // Check for "?" or blanks being inputted

            if (!string.IsNullOrWhiteSpace(sorted)) 
            {
                SpawnWord(sorted);
            }
        }

        bubbleRoyale.AddRange(bubbleList);
        characterNum = 0;
    }
}
