using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    /*
     * Level Manager Class
     * For configuring level settings, plus storing global variable references
     */

    public bool IsPaused;
    public Text PauseText;

    [Space(10)]
    [Header("Level Settings")]
    public bool IsMenu;

    // Gameplay objects
    [HideInInspector] public PlayerController player;
    [HideInInspector] public Camera cam;

    // UI objects
    FadeAnimator fade;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;

        // Find all relevant gameplay objects if we know we aren't in the titlescreen
        if (!IsMenu)
        {
            player = FindObjectOfType<PlayerController>();
        }

        cam = Camera.main;
        fade = FindObjectOfType<FadeAnimator>();
    }

    void Start()
    {
        //AudioManager.instance.StartPlayingMusic(Enumerations.MusicType.MAIN_MENU);
    }

    // Transition into a new scene
    public void ChangeScene(int sceneID)
    {
        StartCoroutine(fade.StartFade(sceneID));
    }

    void Update()
    {
        if (!IsMenu)
        {
            // Pausing game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPaused = !IsPaused;
                PauseText.gameObject.SetActive(IsPaused);
                if (IsPaused)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            }
        }
    }
}
