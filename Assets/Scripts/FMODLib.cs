using FMODUnity;
using UnityEngine;

public class FMODLib : MonoBehaviour
{
    public static FMODLib instance;

    [field: Space(5)]
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference introMusic { get; private set; }
    [field: SerializeField] public EventReference outroMusic { get; private set; }
    [field: SerializeField] public EventReference loseMusic { get; private set; }
    [field: SerializeField] public EventReference atmos { get; private set; }

    [field: Space(5)]
    [field: Header("Throwables")]
    [field: SerializeField] public EventReference jar { get; private set; }
    [field: SerializeField] public EventReference metal { get; private set; }
    [field: SerializeField] public EventReference tomato { get; private set; }
    [field: SerializeField] public EventReference coins { get; private set; }
    [field: SerializeField] public EventReference diamond { get; private set; }
    [field: SerializeField] public EventReference roses { get; private set; }
    [field: SerializeField] public EventReference throwObject { get; private set; }

    [field: Space(5)]
    [field: Header("Crowd - Discourage")]
    [field: SerializeField] public EventReference booing { get; private set; }
    [field: SerializeField] public EventReference crickets { get; private set; }
    [field: SerializeField] public EventReference negativePhrase { get; private set; }
    [field: SerializeField] public EventReference whispering { get; private set; }

    [field: Space(5)]
    [field: Header("Crowd - Encourage")]
    [field: SerializeField] public EventReference clapping { get; private set; }
    [field: SerializeField] public EventReference laughing { get; private set; }
    [field: SerializeField] public EventReference positivePhrase { get; private set; }
    [field: SerializeField] public EventReference woohoo { get; private set; }

    [field: Space(5)]
    [field: Header("Menu")]
    [field: SerializeField] public EventReference choice { get; private set; }
    [field: SerializeField] public EventReference scrolling { get; private set; }
    [field: SerializeField] public EventReference select { get; private set; }

    [field: Space(5)]
    [field: Header("Punchlines")]
    [field: SerializeField] public EventReference confetti { get; private set; }
    [field: SerializeField] public EventReference drums { get; private set; }

    [field: Space(5)]
    [field: Header("Score")]
    [field: SerializeField] public EventReference highScore { get; private set; }

    [field: Space(5)]
    [field: Header("Trap")]
    [field: SerializeField] public EventReference deathWarning { get; private set; }
    [field: SerializeField] public EventReference death { get; private set; }

    [field: Space(5)]
    [field: Header("User Input")]
    [field: SerializeField] public EventReference answerRight { get; private set; }
    [field: SerializeField] public EventReference answerWrong { get; private set; }

    [field: Space(5)]
    [field: Header("Misc.")]
    [field: SerializeField] public EventReference mouseInteracion { get; private set; }

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
