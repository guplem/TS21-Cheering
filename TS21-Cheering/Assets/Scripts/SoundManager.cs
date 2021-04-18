using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private int defaultAudioSourcesQty;
    [HideInInspector] private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();

        foreach (AudioSource audioSource in GetComponents<AudioSource>())
            audioSources.Add(audioSource);

        for (int i = audioSources.Count; i < defaultAudioSourcesQty; i++)
            AddAudioSource();
    }


    //ToDo: Check if the limit of the pitch and pitchRandomization is -3 to 3
    public void PlaySound(AudioClip clip, float volume = 1, bool loop = false, float pitch = 1, AudioMixerGroup audioMixerGroup = null)
    {
        if (clip == null)
        {
            Debug.LogWarning("Trying to play a null clip", this);
            return;
        }

        ConfigureAudioSource(GetFreeAudioSource(), clip, volume, loop, pitch, audioMixerGroup).Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return AddAudioSource();
    }

    // ToDo: check if this is the way is wanted to be the default configuration
    private AudioSource AddAudioSource()
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;
        //newAudioSource.spatialBlend = 1f;
        //newAudioSource.rolloffMode = AudioRolloffMode.Linear;
        //newAudioSource.minDistance = 0.0f;
        //newAudioSource.maxDistance = 30.0f; 
        audioSources.Add(newAudioSource);

        return newAudioSource;
    }

    private AudioSource ConfigureAudioSource(AudioSource audioSource, AudioClip clip, float volume = 1, bool loop = false, float pitch = 1, AudioMixerGroup audioMixerGroup = null)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.pitch = pitch;

        audioSource.loop = loop;

        return audioSource;
    }

    public void StopAllSounds(bool fadeOut = false)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (fadeOut)
            {
                //ToDo: Be able to stop the sounds with any type of animation curve
                FadeOutAudioSource(audioSource);
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
    private void FadeOutAudioSource(AudioSource audioSource)
    {
        StartCoroutine(LowerVolumeAndStopSounds(audioSource));
    }

    private static IEnumerator LowerVolumeAndStopSounds(AudioSource audioSource)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.12f);

        }
        audioSource.Stop();
        audioSource.volume = 1.0f;
    }
    
}
