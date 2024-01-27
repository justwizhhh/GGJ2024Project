using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int maxHealth = 100;
    public int currentHealth;
    public ScoreBoard scoreBoard;
    public int currentScore = 0;
    public int highScore;
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private SpriteRenderer playerRenderer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        EventHandler.ChangeHealth += ChangeHealth;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        scoreBoard.setScore(currentScore, highScore);
    }

    void ChangeHealth(int changeValue)
    {
        currentHealth += changeValue;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("YOU are dead. Not big suprise");
            EventHandler.OnDeath();
        }
        //healthBar.setHealth();
        UpdateSprite();
    }


    [ContextMenu("Take Damage")]
    void TakeTestDamage() 
    {
        ChangeHealth(-1);
    }

    public void UpdateSprite() 
    {
        playerRenderer.sprite = playerSprites[currentHealth / 2 + 1];
    }

    public void ChangeScore(float multiplier)
    {
        currentScore += (int)(UnityEngine.Random.Range(1.0f, 15.0f) * multiplier);
        if (currentScore >= 99999999)
        {
            currentScore = 99999999;
        }
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
        scoreBoard.setScore(currentScore, highScore);
    }
}
