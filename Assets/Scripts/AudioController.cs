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
    Transmission,
    Meteoroid,
}

/// <summary>
/// Audio controller.
/// </summary>
public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("Music Player")]
    public AudioSource MusicPlayer;

    public MusicType CurrentMusicType;

    public AudioClip IntroMusic;
    public AudioClip GameMusic;
    public AudioClip OutroMusic;

    [Header("Sound Player")]
    public AudioSource SoundPlayer;

    public AudioClip CollectSound;
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

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
	{
        PlayMusic(CurrentMusicType);
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
    }

    // Play sound.
    public void PlaySound(SoundType soundType)
    {
        AudioClip soundClip = CollectSound;

        switch (soundType)
        {
            case SoundType.Collect:
            {
                soundClip = CollectSound;
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

        SoundPlayer.volume = Random.Range(VolumeVarianceMin, VolumeVarianceMax);
        SoundPlayer.pitch = Random.Range(PitchVarianceMin, PitchVarianceMax);
        
        SoundPlayer.PlayOneShot(soundClip);
    }
}