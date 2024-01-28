using UnityEngine;

[CreateAssetMenu(fileName = "NewJoke", menuName = "Scriptable Objects/NewJokeObject")]
public class JokeSO : ScriptableObject
{
    public int jokeId;
    public string jokeQuestion;
    public string jokePunchline;
}
