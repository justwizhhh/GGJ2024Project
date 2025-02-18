using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowableObject : MonoBehaviour
{
    public Transform target;
    public float arcHeight = 5f;
    public float throwSpeed = 5f;
    public float spinSpeed = 180f;
    [SerializeField] private ParticleSystem splashParticle;

    private Vector3 startPosition;
    private float startTime;
    private bool isThrown = false;


    private void OnDestroy()
    {
        if (splashParticle != null)
        {
            GameObject.Instantiate(splashParticle, transform.position, transform.rotation);
        }
    }


    void Awake()
    {
        startPosition = transform.position;

        // Set the initial position and visibility
        transform.rotation = Quaternion.identity; // Reset rotation
        isThrown = false;

        //Debug.Log("ThrowableObject initialized.");
    }

    void Update()
    {
        if (!isThrown)
        {
            float elapsedTime = Time.time;

            if (target != null)
            {
                startTime = Time.time;
                isThrown = true;
            }
        }
        else
        {
            float timeFactor = (Time.time - startTime) * throwSpeed;

            if (timeFactor <= 1f)
            {
                // Calculate the position of the throwable object based on a parabolic arc
                float distance = Vector3.Distance(startPosition, target.position);
                float height = Mathf.Sin(timeFactor * Mathf.PI) * arcHeight;

                Vector3 arcPosition = Vector3.Lerp(startPosition, target.position, timeFactor);
                arcPosition.y += height;

                // Update the position of the throwable object
                transform.position = arcPosition;

                // Rotate the throwable object around the z-axis
                transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
                //Debug.Log("ThrowableObject destroyed.");
            }
        }
    }
}
