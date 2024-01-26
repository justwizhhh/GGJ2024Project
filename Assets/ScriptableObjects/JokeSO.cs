using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJoke", menuName = "Interaction/NewJokeObject")]
public class JokeSO : ScriptableObject
{
    public int jokeId;
    public string jokeName;
    public string jokeText;
}
