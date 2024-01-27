using UnityEngine;
using System.Collections;

public class ThrowableManager : MonoBehaviour
{
    public AudienceApproval audienceApproval; // Reference to the AudienceApproval script
    public Transform[] throwableSpawnLocations;
    public GameObject throwablePrefabPositive;
    public GameObject throwablePrefabNegative;
    public int maxThrowables = 5;

    private GameObject player;
    private int activeThrowables = 0;

    public float minInterval = 5f;
    public float maxInterval = 15f;

    public float minIntervalModifier = 0.01f; // Adjusted modifier for closer to 0
    public float maxIntervalModifier = 0.2f;  // Adjusted modifier for closer to 100

    private float originalMinInterval;
    private float originalMaxInterval;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Make sure it has the 'Player' tag.");
        }

        if (audienceApproval == null)
        {
            Debug.LogError("AudienceApproval reference is not set in ThrowableManager!");
        }

        originalMinInterval = minInterval;
        originalMaxInterval = maxInterval;

        InitializeThrowables(1);
        StartCoroutine(SpawnThrowablesRandomly());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int additionalThrowables = Random.Range(1, 4);
            IncreaseThrowables(additionalThrowables);
        }
    }

    void InitializeThrowables(int numThrowables)
    {
        for (int i = 0; i < numThrowables; i++)
        {
            SpawnThrowable();
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
            GameObject throwablePrefab = DetermineThrowablePrefab();

            GameObject throwable = Instantiate(throwablePrefab, spawnLocation.position, Quaternion.identity);
            ThrowableObject throwableObject = throwable.GetComponent<ThrowableObject>();

            if (throwableObject != null)
            {
                throwableObject.target = player.transform;
            }
        }
        else
        {
            Debug.LogWarning("Spawn location already has a throwable.");
        }
    }

    GameObject DetermineThrowablePrefab()
    {
        float approvalRating = audienceApproval.slider.value;
        float positiveEffect = audienceApproval.GetPositiveEffect(approvalRating);
        float negativeEffect = audienceApproval.GetNegativeEffect(approvalRating);

        if (positiveEffect >= negativeEffect)
        {
            return throwablePrefabPositive;
        }
        else
        {
            return throwablePrefabNegative;
        }
    }

    IEnumerator SpawnThrowablesRandomly()
    {
        while (true)
        {
            AdjustInterval();

            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            if (activeThrowables < maxThrowables)
            {
                SpawnThrowable();
                activeThrowables++;
            }
        }
    }

    void AdjustInterval()
    {
        float approvalRating = audienceApproval.slider.value;

        // Adjust the interval modifiers based on approval rating
        float modifier = 1f;

        if (approvalRating < 5 || approvalRating > 95)
        {
            modifier = Mathf.Lerp(minIntervalModifier, maxIntervalModifier, Mathf.Abs(approvalRating - 50) / 50f);
        }

        minInterval = originalMinInterval * modifier;
        maxInterval = originalMaxInterval * modifier;
    }
}
