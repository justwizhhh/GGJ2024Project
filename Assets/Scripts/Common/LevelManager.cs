using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
     * Level Manager Class
     * For configuring level settings, plus storing global variable references
     */

    public bool IsMenu;

    [Space(10)]
    [Header("Level Settings")]
    public string StartMusic;

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
        if (IsMenu)
        {
            player = FindObjectOfType<PlayerController>();
        }

        cam = Camera.main;
        fade = FindObjectOfType<FadeAnimator>();
    }

    void Start()
    {
        if (StartMusic != null) { AudioManager.instance.PlayMusic(StartMusic, 1); }
    }

    // Transition into a new scene
    public void ChangeScene(int sceneID)
    {
        StartCoroutine(fade.StartFade(sceneID));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(AudioManager.instance.MusicSource.volume);
        }
    }
}
