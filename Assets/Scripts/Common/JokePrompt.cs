using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JokePrompt : MonoBehaviour
{
    public string CurrentJoke;

    private List<SpawnedBubble> bubbleList;
    private List<string> nounList;
    private string typeProgress;
    private int typePoint;

    Event input;
    TMP_Text tmptext;
    TMP_FontAsset tmpfont;
    BubbleManager bm;

    private void Awake()
    {
        bm = FindObjectOfType<BubbleManager>();
        
        tmptext = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        NewJoke(CurrentJoke);
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
                    EventHandler.OnLetterTyped(letter);
                    if (char.ToLower(letter) == CurrentJoke[typePoint] || letter == CurrentJoke[typePoint])
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

    public void NewJoke(string joke)
    {
        tmptext.text = CurrentJoke;

        bubbleList = bm.bubbleList;
        foreach (SpawnedBubble bubble in bubbleList)
        {
            nounList.Add(bubble.TMPRO.text);
        }

        typePoint = 0;
        typeProgress = "";
    }

    public Vector2 GetSentencePosition()
    {
        return tmptext.transform.position;
    }
}
