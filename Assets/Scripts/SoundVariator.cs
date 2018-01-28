using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound variator. Sets random volume and pitch to AudioSource.
/// </summary>
public class SoundVariator : MonoBehaviour
{
    [Range(0f, 1f)]
    public float VolumeMin = 0.75f;

    [Range(0f, 1f)]
    public float VolumeMax = 1.0f;

    [Range(-3f, 3f)]
    public float PitchMin = 0.75f;

    [Range(-3f, 3f)]
    public float PitchMax = 1.25f;

    private AudioSource audioSource;

    // Start.
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.volume = Random.Range(VolumeMin, VolumeMax);
            audioSource.pitch = Random.Range(PitchMin, PitchMax);
        }
    }
}