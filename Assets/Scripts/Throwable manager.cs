using UnityEngine;
using System.Collections;

public class ThrowableManager : MonoBehaviour
{
    public AudienceApproval audienceApproval; // Reference to the AudienceApproval script
    public Transform[] throwableSpawnLocations;
    public GameObject throwablePrefabPositive;
    public GameObject throwablePrefabNegative;

    private GameObject player;

    private float timer = 0;

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

    void InitializeThrowables(int numThrowables)
    {
        for (int i = 0; i < numThrowables; i++)
        {
            SpawnThrowable();
        }
    }

    void IncreaseThrowables(int additionalThrowables)
    {
        /*
        if (activeThrowables + additionalThrowables <= maxThrowables)
        {
            InitializeThrowables(additionalThrowables);
            activeThrowables += additionalThrowables;
        }
        else
        {
            Debug.LogWarning("Cannot exceed the maximum number of throwables.");
        }*/
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

            GameObject throwablePrefab;
            if (DetermineThrowablePrefab())
            {
                throwablePrefab = throwablePrefabPositive;
            }
            else 
            {
                throwablePrefab = throwablePrefabNegative;
                //throwablePrefab.GetComponent<NegativeThrowable>().damage = 300;
            }
            

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

    bool DetermineThrowablePrefab()
    {
        float approvalRating = audienceApproval.slider.value;
        float positiveEffect = audienceApproval.GetPositiveEffect(approvalRating);
        float negativeEffect = audienceApproval.GetNegativeEffect(approvalRating);

        if (positiveEffect >= negativeEffect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SpawnThrowablesRandomly()
    {
        AdjustInterval();

        float interval = Random.Range(minInterval, maxInterval);
        Debug.Log(interval);
        yield return new WaitForSeconds(interval);

        SpawnThrowable();

        StartCoroutine(SpawnThrowablesRandomly());
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
