using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private Transform[] audiencePositions;
    [SerializeField] private string fakeJokes;
    [SerializeField] private float floatingSpeed = 1;
    [SerializeField] private float maxheight;
    [SerializeField] private float minheight;

    public List<SpawnedBubble> bubbleList = new List<SpawnedBubble>();
    // Start is called before the first frame update
    void Start()
    {
        TestSpawn();
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
                bubble.rigidBody.velocity = new Vector2(bubble.rigidBody.velocity.x, 0.1f);
            }
            else 
            {
                bubble.rigidBody.gravityScale = -0.03f * floatingSpeed;
            }
        }
    }

    [ContextMenu("Test Spawn")]
    void TestSpawn() 
    {
        StartJoke("Why did the chicken cross the road?");
    }


    void SpawnWord(string word) 
    {
        Vector3 spawnPos = audiencePositions[Random.Range(0, audiencePositions.Length - 1)].transform.position;
        SpawnedBubble newBubble = GameObject.Instantiate(bubble, spawnPos, Quaternion.identity, this.transform).GetComponent<SpawnedBubble>();

        //Intalise Bubble
        newBubble.TMPRO.text = word;
        newBubble.rigidBody.gravityScale = -0.03f * floatingSpeed;
        
        // Get width of the TMPRO
        
        bubbleList.Add(newBubble);
    }

    
    void StartJoke(string Joke) 
    {
        string[] words = Joke.Split(' ');

        foreach (string word in words) 
        {
            SpawnWord(word);
        }

    }
}
