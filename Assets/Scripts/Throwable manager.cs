using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwablemanager : MonoBehaviour
{
    public Transform[] throwableSpawnLocations;
    public GameObject throwablePrefab;
    public int maxThrowables = 5;

    private GameObject player; // Reference to the player object
    private int activeThrowables = 0;

    public float minInterval = 5f; // Minimum interval between throwables
    public float maxInterval = 15f; // Maximum interval between throwables
    public int maxThrowablesAtOnce = 3; // Maximum throwables spawned at once

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Make sure it has the 'Player' tag.");
        }

        InitializeThrowables(1); // Start with 1 throwable

        // Start invoking the method to spawn throwables at random intervals
        InvokeRepeating("SpawnThrowablesAtInterval", Random.Range(minInterval, maxInterval), Random.Range(minInterval, maxInterval));
    }

    void Update()
    {
        // For testing purposes, increase the number of throwables when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int additionalThrowables = Random.Range(1, 4); // Randomly add 1 to 3 throwables
            IncreaseThrowables(additionalThrowables);
        }
    }

    void InitializeThrowables(int numThrowables)
    {
        for (int i = 0; i < numThrowables; i++)
        {
            SpawnThrowable();
            Debug.Log("ThrowableManager initialized throwable.");
        }
    }

    void IncreaseThrowables(int additionalThrowables)
    {
        if (activeThrowables + additionalThrowables <= maxThrowables)
        {
            InitializeThrowables(additionalThrowables);
            activeThrowables += additionalThrowables;
        }
        else
        {
            Debug.LogWarning("Cannot exceed the maximum number of throwables.");
        }
    }

    void SpawnThrowable()
    {
        if (throwableSpawnLocations.Length == 0)
        {
            Debug.LogError("No throwable spawn locations assigned.");
            return;
        }

        int randomSpawnIndex = Random.Range(0, throwableSpawnLocations.Length);
        Transform spawnLocation = throwableSpawnLocations[randomSpawnIndex];

        if (spawnLocation.childCount == 0)
        {
            GameObject throwable = Instantiate(throwablePrefab, spawnLocation.position, Quaternion.identity);
            Throwable throwableObject = throwable.GetComponent<Throwable>();

            if (throwableObject != null)
            {
                throwableObject.target = player.transform; // Set the player object as the target
                Debug.Log("ThrowableManager spawned throwable at position: " + spawnLocation.position);
            }
        }
        else
        {
            Debug.LogWarning("Spawn location already has a throwable.");
        }
    }

    // Method to spawn multiple throwables at different spawn locations
    void SpawnThrowablesAtInterval()
    {
        if (throwableSpawnLocations.Length == 0)
        {
            Debug.LogError("No throwable spawn locations assigned.");
            return;
        }

        for (int i = 0; i < maxThrowablesAtOnce; i++)
        {
            int randomSpawnIndex = Random.Range(0, throwableSpawnLocations.Length);
            Transform spawnLocation = throwableSpawnLocations[randomSpawnIndex];

            if (spawnLocation.childCount == 0)
            {
                GameObject throwable = Instantiate(throwablePrefab, spawnLocation.position, Quaternion.identity);
                Throwable throwableObject = throwable.GetComponent<Throwable>();

                if (throwableObject != null)
                {
                    throwableObject.target = player.transform; // Set the player object as the target
                    Debug.Log("ThrowableManager spawned throwable at position: " + spawnLocation.position);
                }
            }
            else
            {
                Debug.LogWarning("Spawn location already has a throwable.");
            }
        }
    }
}