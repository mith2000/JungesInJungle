using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        public bool loop = false;

        public SoundType type = SoundType.SFX;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(.1f, 3f)] public float pitch = 1f;

        [HideInInspector] public AudioSource source;

        public enum SoundType
        {
            Music = 0,
            SFX = 1
        }
    }

    public float musicVolumn = 0.5f;
    public float SFXVolumn = 1f;

    public Sound[] sounds;

    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        musicVolumn = PlayerPrefs.GetFloat("MusicVolumn");
        SFXVolumn = PlayerPrefs.GetFloat("SFXVolumn");

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.type == Sound.SoundType.Music)
            {
                s.source.volume = s.volume * musicVolumn;
            }
            else if (s.type == Sound.SoundType.SFX)
            {
                s.source.volume = s.volume * SFXVolumn;
            }
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void Play(string name, bool oneshot = true)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (!oneshot)
        {
            s.source.Play();
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.UnPause();
    }

    public void MusicVolumn_OnChange()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == Sound.SoundType.Music)
            {
                s.source.volume = s.volume * musicVolumn;
            }
        }
    }

    public void SFXVolumn_OnChange()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == Sound.SoundType.SFX)
            {
                s.source.volume = s.volume * SFXVolumn;
            }
        }
    }
}
