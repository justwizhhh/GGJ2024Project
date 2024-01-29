
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text scoreText;

    public void setScore(int value, int hiScoreValue)
    {
        scoreText.SetText("SCORE: " + value.ToString() + "\n \n <b>HIGH SCORE: " + hiScoreValue + "</b>");
    }
}
