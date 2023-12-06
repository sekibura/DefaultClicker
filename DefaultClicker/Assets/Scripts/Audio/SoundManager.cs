using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    [SerializeField]
    private SoundAudioClip[] _soundAudioClips;
    private GameObject _soundGameObject;
    AudioSource audioSource;
    public enum Sound
    {
        None,
        Click
    }

    void Start()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(Sound sound)
    {
        if (!_soundGameObject)
        {
            _soundGameObject = new GameObject("Sound");
            audioSource = _soundGameObject.AddComponent<AudioSource>();
        }
        var audioClip = GetAudioClip(sound);
        audioSource.outputAudioMixerGroup = audioClip.mixerGroup;
        audioSource.PlayOneShot(audioClip.audioClip);
    }

    private SoundAudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAudioClip clip in _soundAudioClips)
        {
            if (clip.sound == sound)
                return clip;
        }
        Debug.LogError($"Souns {sound} - not found!");
        return null;
    }

    [Serializable]
    public class SoundAudioClip 
    {
        public Sound sound;
        public AudioClip audioClip;
        public  AudioMixerGroup mixerGroup;
    }
}
