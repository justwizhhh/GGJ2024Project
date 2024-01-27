using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeThrowable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Negative Action:
            EventHandler.OnChangeHealth(-25);


            //Debug.Log("ThrowableObject touched player and destroyed.");
        }
    }
}
