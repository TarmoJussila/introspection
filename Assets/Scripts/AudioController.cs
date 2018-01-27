using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Music types.
/// </summary>
public enum MusicType
{
    Intro,
    Game,
    Outro
}

/// <summary>
/// Sound types.
/// </summary>
public enum SoundType
{
    Collect,
    Objective,
    Transmission,
    Meteoroid,
}

/// <summary>
/// Audio controller. Persistent.
/// </summary>
public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("Ambient Player")]
    public AudioSource AmbientPlayer;

    public AudioClip Ambient;

    [Header("Music Player")]
    public AudioSource MusicPlayer;

    public MusicType CurrentMusicType;

    public AudioClip IntroMusic;
    public AudioClip GameMusic;
    public AudioClip OutroMusic;

    [Header("Sound Player")]
    public AudioSource SoundPlayer;

    public AudioClip CollectSound;
    public AudioClip ObjectiveSound;
    public AudioClip TransmissionSound;
    public AudioClip MeteoroidSound;

    [Range(0.0f, 1.0f)]
    public float VolumeVarianceMin = 0.8f;

    [Range(0.0f, 1.0f)]
    public float VolumeVarianceMax = 1.0f;

    [Range(-3.0f, 3.0f)]
    public float PitchVarianceMin = 0.8f;

    [Range(-3.0f, 3.0f)]
    public float PitchVarianceMax = 1.2f;

    // Awake. Persistent.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start.
    private void Start()
	{
        PlayAmbient();
        PlayMusic(CurrentMusicType);
	}

    // Play ambient.
    public void PlayAmbient()
    {
        AmbientPlayer.clip = Ambient;

        AmbientPlayer.Play();
    }
	
    // Play music (change track).
	public void PlayMusic(MusicType musicType)
    {
        CurrentMusicType = musicType;

        AudioClip musicClip = IntroMusic;

        switch (musicType)
        {
            case MusicType.Intro:
            {
                musicClip = IntroMusic;
                break;
            }
            case MusicType.Game:
            {
                musicClip = GameMusic;
                break;
            }
            case MusicType.Outro:
            {
                musicClip = OutroMusic;
                break;
            }
        }

        MusicPlayer.clip = musicClip;

        MusicPlayer.Play();
    }

    // Play sound (with given pitch).
    public void PlaySound(SoundType soundType, float pitchAmount)
    {
        PlaySound(soundType, false, pitchAmount);
    }

    // Play sound (with given pitch and volume).
    public void PlaySound(SoundType soundType, float pitchAmount, float volumeAmount)
    {
        PlaySound(soundType, false, pitchAmount, false, volumeAmount);
    }

    // Play sound.
    public void PlaySound(SoundType soundType, bool isRandomPitch = true, float pitchAmount = 1.0f, bool isRandomVolume = true, float volumeAmount = 1.0f)
    {
        AudioClip soundClip = CollectSound;

        switch (soundType)
        {
            case SoundType.Collect:
            {
                soundClip = CollectSound;
                break;
            }
            case SoundType.Objective:
            {
                soundClip = ObjectiveSound;
                break;
            }
            case SoundType.Transmission:
            {
                soundClip = TransmissionSound;
                break;
            }
            case SoundType.Meteoroid:
            {
                soundClip = MeteoroidSound;
                break;
            }
        }

        if (isRandomVolume)
        {
            volumeAmount = Random.Range(VolumeVarianceMin, VolumeVarianceMax);
        }

        if (isRandomPitch)
        {
            pitchAmount = Random.Range(PitchVarianceMin, PitchVarianceMax);
        }

        SoundPlayer.volume = volumeAmount;
        SoundPlayer.pitch = pitchAmount;
        
        SoundPlayer.PlayOneShot(soundClip);
    }
}