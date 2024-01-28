using FMODUnity;
using UnityEngine;

public class FMODLib : MonoBehaviour
{
    public static FMODLib instance;

    [field: Space(5)]
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Space(5)]
    [field: Header("Effects")]
    [field: SerializeField] public EventReference testOneShot { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Audio Library instance");
            return;
        }
        instance = this;
    }
}
