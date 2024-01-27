using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
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
        healthBar.setMaxHealth(maxHealth);
        EventHandler.ChangeHealth += ChangeHealth;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        scoreBoard.setScore(currentScore, highScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeHealth(-15); // damage by 15
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeHealth(15); // damage by 15
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            ChangeScore(1); // score up with multiplier
        }
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
            EventHandler.OnDeath();
        }
        healthBar.setHealth();
        UpdateSprite();
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
