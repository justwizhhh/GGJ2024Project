using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnedBubble : MonoBehaviour
{
    [Header("REQUIRED")]
    [SerializeField] public TextMeshPro TMPRO;
    public string text;
    public Rigidbody2D rigidBody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Call Audio Code
    }
}
