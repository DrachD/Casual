using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    
    private AudioSource audioSource;

    [SerializeField] FloatVariable floatVariable;

    public Action<AudioClip, bool> OnPlaySound;

    /// <summary>
    /// Unity`s Awake method
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        audioSource = GetComponent<AudioSource>();
        OnPlaySound += PlaySound;
    }

    /// <summary>
    /// Unity's Start method.
    /// </summary>
    private void Start()
    {
        audioSource.volume = floatVariable.Value;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void PlaySound(AudioClip sound, bool loop)
    {
        audioSource.clip = sound;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
