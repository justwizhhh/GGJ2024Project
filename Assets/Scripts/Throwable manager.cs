using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System;

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

    private List<PositiveThrowableSO> listOfPositiveThrowables;
    private List<NegativeThrowableSO> listOfNegativeThrowables;

    [Space(10)]
    public Animator ThrowAnim1;
    public Animator ThrowAnim2;


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

        LoadThrowableAssets();

        InitializeThrowables(1);
        StartCoroutine(SpawnThrowablesRandomly());

        ThrowAnim1.gameObject.SetActive(false);
        ThrowAnim2.gameObject.SetActive(false);
    }

    private void LoadThrowableAssets()
    {
        listOfPositiveThrowables = Resources.LoadAll<PositiveThrowableSO>("Throwables/Beneficial").ToList();
        listOfNegativeThrowables = Resources.LoadAll<NegativeThrowableSO>("Throwables/Harmful").ToList();
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

        int randomSpawnIndex = UnityEngine.Random.Range(0, throwableSpawnLocations.Length);
        Transform spawnLocation = throwableSpawnLocations[randomSpawnIndex];

        int audienceSelection = (int)((audienceApproval.slider.value - 50f) / 16); // Selects an item for the audience to use depending on how angry/happy they currently are

        GameObject throwablePrefab;
        if (DetermineThrowablePrefab())
        {
            //Pro
            throwablePrefab = listOfPositiveThrowables[Mathf.Clamp(audienceSelection, 1, 3) - 1].throwable;
        }
        else 
        {
            //Negative
            throwablePrefab = listOfNegativeThrowables[1 - Mathf.Clamp(audienceSelection, -3, -1)].throwable;
        }

        //throwablePrefab = DetermineThrowablePrefab()? throwablePrefabPositive:throwablePrefabNegative;            

        GameObject throwable = Instantiate(throwablePrefab, spawnLocation.position, Quaternion.identity);
        ThrowableObject throwableObject = throwable.GetComponent<ThrowableObject>();

        if (throwableObject != null) throwableObject.target = player.transform;


        // Animations
        int anim = UnityEngine.Random.Range(0, 2);
        if (anim == 0)
        {
            if (!ThrowAnim1.gameObject.activeSelf) { ThrowAnim1.gameObject.SetActive(true); }
            StartCoroutine(AnimStop(ThrowAnim1));
            ThrowAnim1.gameObject.transform.position = spawnLocation.position;
        }
        else
        {
            if (!ThrowAnim2.gameObject.activeSelf) { ThrowAnim2.gameObject.SetActive(true); }
            StartCoroutine(AnimStop(ThrowAnim2));
            ThrowAnim2.gameObject.transform.position = spawnLocation.position;
        }
    }


    IEnumerator AnimStop(Animator anim)
    {
        anim.enabled = true;
        anim.Rebind();
        anim.Update(0f);

        float animTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(animTime);

        anim.enabled = false;
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

        float interval = UnityEngine.Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(interval);

        if (Time.timeScale != 0) // Stop throwables being spawned while paused
            SpawnThrowable();

        StartCoroutine(SpawnThrowablesRandomly());
    }
}
