using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Enumerations;

public class AudioManager : MonoBehaviour
{
    [Header("Volume Settings")]
    public static float MusicVolume = 1;
    public static float SoundVolume = 1;

    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private Vector3 audioPosition;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager instance");
            return;
        }
        instance = this;

        audioPosition = FindObjectOfType<StudioListener>().transform.position;
    }

    void Start()
    {
        StartPlayingMusic(MusicType.GAME_MUSIC);
    }   

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound, audioPosition);
    }

    /// <summary>
    /// Starts to play music of the given music type
    /// </summary>
    /// <param name="musicType"></param>
    /// <param name="allowFadeOut"></param>
    public void StartPlayingMusic(MusicType musicType, bool allowFadeOut = true)
    {
        musicEventInstance.getPlaybackState(out var result);
        if (result == PLAYBACK_STATE.PLAYING)
        {
            StopPlayingMusic(allowFadeOut);
        }

        switch (musicType)
        {
            case MusicType.MAIN_MENU:
                musicEventInstance = CreateEventInstance(FMODLib.instance.menuMusic);
                break;
            case MusicType.GAME_MUSIC:
                musicEventInstance = CreateEventInstance(FMODLib.instance.gameMusic);
                break;
            case MusicType.GAME_OVER:
                musicEventInstance = CreateEventInstance(FMODLib.instance.endOfGameMusic);
                break;
            case MusicType.CREDITS:
                musicEventInstance = CreateEventInstance(FMODLib.instance.creditsMusic);
                break;
            default:
                break;
        }
        
        musicEventInstance.start();
    }

    /// <summary>
    /// Stops playing the music
    /// </summary>
    /// <param name="allowFadeOut"></param>
    public void StopPlayingMusic(bool allowFadeOut)
    {
        musicEventInstance.stop(allowFadeOut? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
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
