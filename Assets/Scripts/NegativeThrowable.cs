using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NegativeThrowable : MonoBehaviour
{
    public MonoBehaviour scriptToDisable;
    [SerializeField] float clickRange = 4;
    [SerializeField] int damamgeMod = 1;

    void Start()
    {
        EventHandler.Click += OnClick;
    }

    private void OnDestroy()
    {
        EventHandler.Click -= OnClick;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Negative Action:
            EventHandler.OnChangeHealth(-damamgeMod);


            //Debug.Log("ThrowableObject touched player and destroyed.");
        }
        else if (collision.CompareTag("Kill Zones")) 
        {
            Destroy(gameObject);
        }
    }

    void OnClick(Vector2 position)
    {
        if (Time.timeScale != 0)
        {
            if (Vector2.Distance(position, new Vector2(transform.position.x, transform.position.y)) < clickRange)
            {
                DisableScript(scriptToDisable);
            }
        }
    }

        public static void DisableScript(MonoBehaviour script)
    {
        if (script != null)
        {
            script.GetComponent<Collider2D>().enabled = false; // Should loop through all child colliders
            script.enabled = false;
        }
        else
        {
            Debug.LogWarning("Script is null! Cannot disable.");
        }
    }
}
