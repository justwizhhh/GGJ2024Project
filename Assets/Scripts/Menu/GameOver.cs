using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    /*
     * Game Over Class
     * For enabling animations and processing more menu button inputs
     */

    public bool isMenuActive;

    [Space(10)]
    public Animator PlayerAnimator;
    public Animator TrapdoorAnimator;

    private BoxCollider2D playerCol;
    private ThrowableManager throwableManager;

    [Space(10)]
    public Text RetryButton;
    public Text BackToTitleButton;
    public Text ScoreDisplayText;

    // Menu options are either Retry (0), or Back-to-Title (1)
    int current_menu_option = 0;

    Animator anim;

    private void Awake()
    {        anim = GetComponent<Animator>();
        EventHandler.PlayerDeath += StartAnim;
    }

    void Start()
    {
        isMenuActive = false;
        anim.enabled = false;
    }

    public void UpdateScoreDisplay(int scoreDisplay)
    {
        ScoreDisplayText.text = "Score: " + scoreDisplay;
    }

    // Animate the player falling into the trapdoor before showing the game-over menu
    public void StartAnim()
    {
        EventHandler.OnSceneChange(Enumerations.GameState.END);
        AudioManager.instance.PlayOneShot(FMODLib.instance.death);

        UpdateScoreDisplay(PlayerData.instance.currentScore);


        playerCol = PlayerAnimator.gameObject.GetComponent<BoxCollider2D>();
        throwableManager = FindObjectOfType<ThrowableManager>();
        playerCol.enabled = false;
        throwableManager.enabled = false;
        throwableManager.StopAllCoroutines();

        PlayerAnimator.SetTrigger("Death");
        TrapdoorAnimator.SetTrigger("Death");

        anim.enabled = true;
        anim.SetTrigger("Start");
    }

    // Enabling menu interaction once the whole animation stops playing
    public void EnableMenu()
    {
        isMenuActive = true;
    }

    public void ChangeMenuPosition(int pos)
    {
        current_menu_option = pos;
    }

    public void Select()
    {
        switch (current_menu_option)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                EventHandler.OnSceneChange(Enumerations.GameState.PLAYING);
                break;

            case 1:
                SceneManager.LoadScene(1);
                EventHandler.OnSceneChange(Enumerations.GameState.INTRO);

                break;
        }
    }

    void Update()
    {
        if (isMenuActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) current_menu_option = 0;
            if (Input.GetKeyDown(KeyCode.DownArrow)) current_menu_option = 1;

            if (Input.GetKeyDown(KeyCode.Return)) Select();

            switch (current_menu_option)
            {
                case 0:
                    RetryButton.text = "> Retry <";
                    BackToTitleButton.text = "Back to Title";
                    break;

                case 1:
                    RetryButton.text = "Retry";
                    BackToTitleButton.text = "> Back to Title <";
                    break;
            }
        }
    }
}
