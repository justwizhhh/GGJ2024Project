using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeThrowable : MonoBehaviour
{
    public MonoBehaviour scriptToDisable;

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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (scriptToDisable != null)
                {
                    DisableScript(scriptToDisable);
                }
            }
        }
    }

    public static void DisableScript(MonoBehaviour script)
    {
        if (script != null)
        {
            script.enabled = false;
        }
        else
        {
            Debug.LogWarning("Script is null! Cannot disable.");
        }
    }
}
