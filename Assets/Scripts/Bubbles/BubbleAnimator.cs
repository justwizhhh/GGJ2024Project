using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleAnimator : MonoBehaviour
{
    [Tooltip("Assigned at Runtime via BubbleManager")]
    public Transform Target;
    public TextMeshPro text;
    public AnimationCurve PoppingCurve;
    [SerializeField] private ParticleSystem popEffect;
    [SerializeField] private float animationSpeed = 1;
    private float aliveTimer = 0;

    private Vector3 startPos;
    private Quaternion startRot;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(popEffect, transform.position, transform.rotation).Play();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, Target.transform.position, PoppingCurve.Evaluate(aliveTimer * animationSpeed));
        transform.rotation = Quaternion.Lerp(startRot, Target.transform.rotation, PoppingCurve.Evaluate(aliveTimer * animationSpeed));
        aliveTimer += Time.deltaTime;

        if (animationSpeed * aliveTimer > 1) // If the timer reaches the end of the curve
        {
            ScribeManager.instance.UpdateTMPRO();
            Destroy(this.gameObject);
        }
    }
}
