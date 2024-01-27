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
    public Text RetryButton;
    public Text BackToTitleButton;
    public Text ScoreDisplayText;

    // Menu options are either Retry (0), or Back-to-Title (1)
    int current_menu_option = 0;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        isMenuActive = false;
        //anim.enabled = false;
    }

    public void UpdateScoreDisplay(int scoreDisplay)
    {
        ScoreDisplayText.text = "Score: " + scoreDisplay;
    }

    public void StartAnim()
    {
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
                break;

            case 1:
                SceneManager.LoadScene(1);
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
