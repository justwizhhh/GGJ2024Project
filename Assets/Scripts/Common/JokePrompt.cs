using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JokePrompt : MonoBehaviour
{
    public string CurrentJoke;

    private List<SpawnedBubble> bubbleList;
    private string typeProgress;
    private int typePoint;

    Event input;
    TMP_Text tmptext;
    BubbleManager bm;

    private void Awake()
    {
        bm = FindObjectOfType<BubbleManager>();
        
        tmptext = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        tmptext.text = CurrentJoke;
        bubbleList = bm.bubbleList;
    }

    private void OnGUI()
    {
        input = Event.current;
    }

    private void Update()
    {
        if (input.isKey)
        {
            // Check for correct letter
            string key = input.keyCode.ToString();
            char letter = key.ToCharArray()[0];
            if (key != "None")
            {
                // Check for correct letters
                if (char.ToLower(letter) == CurrentJoke[typePoint] || letter == CurrentJoke[typePoint])
                {
                    typePoint++;
                    typeProgress += letter.ToString().ToLower();
                }
                else
                {
                    if (key == "Space" && CurrentJoke[typePoint].ToString() == " ")
                    {
                        typePoint++;
                        typeProgress += " ";
                    }
                }

                if (typeProgress == CurrentJoke)
                {
                    Debug.Log("Well done okay");
                }
            }
        }
    }
}
