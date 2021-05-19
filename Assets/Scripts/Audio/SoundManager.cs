using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public List<AudioClip> sounds;

    private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();
    
    public static SoundManager instance;

    private BackgroundMusic bgMusic;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (AudioClip sound in sounds)
        {
            nameToSound.Add(sound.name , sound);
        }

        bgMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
        PlaySound("limbo", true);
    }

    public void PlaySound(string soundName, bool loop = false)
    {
        AudioClip clip = nameToSound[soundName];
        if (clip != null)
        {
            bgMusic?.OnPlaySound(clip, loop);
        }
    }
}
