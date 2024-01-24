using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickToFocus : MonoBehaviour
{
    /*
     * Click-to-Focus Class
     * To ensure the player is focusing on the game window, and to play logo animation(s)
     */

    public Button button;
    public new Animation animation;


    public void PlayIntro()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        button.interactable = false;

        animation.gameObject.SetActive(true);
        animation.Play();
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        float anim_length = animation.clip.length;
        yield return new WaitForSeconds(anim_length);
        SceneManager.LoadScene(1);
    }
}
