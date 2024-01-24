using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAnimator : MonoBehaviour
{
    /*
     * Fade Animator Class
     * For starting and stopping the fade animations that play inbetween scene-transitions
     */

    Animator anim;
    Image image;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        image.enabled = true;
    }

    public IEnumerator StartFade(int sceneID)
    {
        anim.SetTrigger("FadeOut");

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        float clipLength = clipInfo[0].clip.length;

        yield return new WaitForSeconds(clipLength);
        SceneManager.LoadScene(sceneID);
    }
}
