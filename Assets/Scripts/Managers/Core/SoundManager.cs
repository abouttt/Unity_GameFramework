using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Enums.Sound.Count];
    private Dictionary<string, AudioClip> _audioClips = new();

    private Transform _root = null;

    public void Init()
    {
        if (!_root)
        {
            _root = new GameObject { name = "[Sound_Root]" }.transform;
            Object.DontDestroyOnLoad(_root);

            var soundTypeNames = System.Enum.GetNames(typeof(Enums.Sound));
            for (int i = 0; i < soundTypeNames.Length - 1; i++)
            {
                var go = new GameObject { name = soundTypeNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = _root;
            }

            _audioSources[(int)Enums.Sound.Bgm].loop = true;
        }
    }

    public void Play(string path, Enums.Sound soundType, float volume = 1f)
    {
        var audioClip = getAudioClip(path);
        Play(audioClip, soundType, volume);
    }

    public void Play(AudioClip audioClip, Enums.Sound soundType, float volume = 1f)
    {
        if (!audioClip)
        {
            return;
        }

        var audioSource = _audioSources[(int)soundType];

        if (soundType == Enums.Sound.Bgm)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Stop(Enums.Sound soundType) => _audioSources[(int)soundType].Stop();

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }

    private AudioClip getAudioClip(string path)
    {
        if (!path.Contains("Sounds/"))
        {
            path = $"Sounds/{path}";
        }

        if (_audioClips.TryGetValue(path, out var audioClip))
        {
            return audioClip;
        }

        audioClip = Managers.Resource.Load<AudioClip>(path);
        _audioClips.Add(path, audioClip);

        return audioClip;
    }
}
