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
        if (input.isKey)
        {
            // Check for correct letter
            string key = input.keyCode.ToString();
            char letter = key.ToCharArray()[0];
            if (key != "None")
            {
                if (key == "Space")
                {
                    typeProgress += " ";
                }
                else
                {
                    if (char.ToLower(letter) == CurrentJoke[typePoint] || letter == CurrentJoke[typePoint])
                    {
                        typeProgress += letter.ToString();
                    }
                }

                Debug.Log(letter);
            }
        }
    }

    
    void Update()
    {
        
    }
}
