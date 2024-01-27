using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public ScoreBoard scoreBoard;
    public int currentScore = 0;
    public int highScore;

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
            Debug.Log("Dead!!");
        }
        healthBar.setHealth(currentHealth);
    }

    void ChangeScore(int multiplier)
    {
        currentScore += (UnityEngine.Random.Range(1, 15) * multiplier);
        if (currentScore >= 999999)
        {
            currentScore = 999999;
        }
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
        scoreBoard.setScore(currentScore, highScore);
    }
}
