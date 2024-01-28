using UnityEngine;
using System.Collections;

public class ThrowableManager : MonoBehaviour
{
    public AudienceApproval audienceApproval; // Reference to the AudienceApproval script
    public Transform[] throwableSpawnLocations;
    public GameObject throwablePrefabPositive;
    public GameObject throwablePrefabNegative;

    private GameObject player;

    public float minInterval = 5f;
    public float maxInterval = 15f;

    public float minIntervalModifier = 0.01f; // Adjusted modifier for closer to 0
    public float maxIntervalModifier = 0.2f;  // Adjusted modifier for closer to 100


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

    void SpawnThrowable()
    {
        if (throwableSpawnLocations.Length == 0)
        {
            Debug.LogError("No throwable spawn locations assigned.");
            return;
        }

        int randomSpawnIndex = Random.Range(0, throwableSpawnLocations.Length);
        Transform spawnLocation = throwableSpawnLocations[randomSpawnIndex];

        GameObject throwablePrefab = DetermineThrowablePrefab()? throwablePrefabPositive:throwablePrefabNegative;            

        GameObject throwable = Instantiate(throwablePrefab, spawnLocation.position, Quaternion.identity);
        ThrowableObject throwableObject = throwable.GetComponent<ThrowableObject>();

        if (throwableObject != null) throwableObject.target = player.transform;
    }

    bool DetermineThrowablePrefab()
    {
        float approvalRating = audienceApproval.slider.value;
        float positiveEffect = audienceApproval.GetPositiveEffect(approvalRating);
        float negativeEffect = audienceApproval.GetNegativeEffect(approvalRating);

        float randomFactor = UnityEngine.Random.Range(0.0f,1.0f);
        float positiveWeightedValue = positiveEffect / (positiveEffect + negativeEffect);

        Debug.Log("Curves: " + positiveEffect + "|" + negativeEffect + " * PosWeight: " + positiveWeightedValue + " | " + randomFactor);

        if (positiveWeightedValue >= randomFactor)
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

        float interval = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(interval);

        SpawnThrowable();

        StartCoroutine(SpawnThrowablesRandomly());
    }
}
