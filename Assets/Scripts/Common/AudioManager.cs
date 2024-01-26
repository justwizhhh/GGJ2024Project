using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*
     * Audio Manager Class
     * For playing sound-effects and music anywhere in the game
     */

    [Header("Volume Settings")]
    public static float MusicVolume = 1;
    public static float SoundVolume = 1;

    public List<AudioClip> MusicClips = new List<AudioClip>();
    public List<AudioClip> SoundClips = new List<AudioClip>();

    [Space(10)]
    public AudioSource MusicSource;
    public List<AudioSource> SoundSources = new List<AudioSource>();

    public static AudioManager instance;

    private void Awake()
    {
        // Singleton instance
        instance = this;
    }

    void Start()
    {
        if (GetComponent<AudioSource>() == null) 
        { 
            MusicSource = gameObject.AddComponent<AudioSource>();
            MusicSource.loop = true;
            MusicSource.volume = MusicVolume;
        }
        
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume += volume;
        MusicVolume = Mathf.Clamp(MusicVolume, 0, 1);

        MusicSource.volume = MusicVolume;
    }

    public void SetSoundVolume(float volume)
    {
        SoundVolume += volume;
        SoundVolume = Mathf.Clamp(SoundVolume, 0, 1);

        foreach (AudioSource source in SoundSources)
        {
            source.volume = SoundVolume;
        }
    }

    // Play one sound-effect
    public IEnumerator PlaySound(string clipName, float volume)
    {
        AudioSource source;
        if (!(source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource))
        {
            Debug.LogError("Sound file not loaded correctly!");
            yield return null;
        }

        SoundSources.Add(source);
        source.clip = SoundClips.Find(clip => clip.name == clipName);
        source.volume = Mathf.Clamp(volume, 0, SoundVolume);

        source.Play();
        yield return new WaitForSeconds(source.clip.length);

        SoundSources.Remove(source);
        Destroy(source);

    }

    public void StopSounds()
    {
        foreach (AudioSource source in SoundSources) { source.Stop(); }
    }

    // Change the currently-playing music
    public void PlayMusic(string clipName, float volume)
    {
        if (!(MusicSource.clip = MusicClips.Find(clip => clip.name == clipName)))
        {
            Debug.LogError("Music file not loaded correctly!");
        }
        else
        {
            MusicSource.volume = Mathf.Clamp(volume, 0, MusicVolume);
            MusicSource.Play();
        }
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
