using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    /*
     * Audio Manager Class
     * For playing sound-effects and music anywhere in the game
     */

    [Header("Volume Settings")]
    public static float MusicVolume = 1;
    public static float SoundVolume = 1;

    [field: Space(10)]
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager instance");
            return;
        }
        instance = this;
    }

    void Start()
    {
         EventHandler.PlayOneShotAudio += PlayOneShot;
        StartMusic(music);
    }
   

    private void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void StartMusic(EventReference music)
    {
        musicEventInstance = CreateEventInstance(music);
        musicEventInstance.start();
    }

    private EventInstance CreateEventInstance(EventReference soundEvent)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(soundEvent);
        return eventInstance;
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume += volume;
        MusicVolume = Mathf.Clamp(MusicVolume, 0, 1);

        //MusicSource.volume = MusicVolume;
    }

    public void SetSoundVolume(float volume)
    {
        SoundVolume += volume;
        SoundVolume = Mathf.Clamp(SoundVolume, 0, 1);

        //foreach (AudioSource source in SoundSources)
        //{
        //    source.volume = SoundVolume;
        //}
    }

    
}
