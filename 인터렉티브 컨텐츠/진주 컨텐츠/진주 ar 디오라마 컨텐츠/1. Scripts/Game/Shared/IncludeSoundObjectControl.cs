using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludeSoundObjectControl : MonoBehaviour
{
    AudioSource _AudioSource;

    [SerializeField] AudioClip[] SoundClips;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(_AudioSource == null)
        {
            _AudioSource = GetComponent<AudioSource>();
        }
        StartCoroutine(PlayIncludeSounds());
    }

    IEnumerator PlayIncludeSounds()
    {
        int i = 0;
        int cnt = SoundClips.Length;
        while(true)
        {
            yield return null;
            if (!_AudioSource.isPlaying)
            {
                if (i == cnt) break;
                yield return new WaitForSeconds(0.5f);
                _AudioSource.clip = SoundClips[i];
                _AudioSource.Play();
                i++;
            }
        }
    }
}
