using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveThrowable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Positive Action
            GrantPoints(1);

            //Debug.Log("ThrowableObject touched player and destroyed.");
        }
    }

    void GrantPoints(float mulitplier) 
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GrantPoints(2.5f);
            }
        }
    }
}