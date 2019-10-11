using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSong : MonoBehaviour
{

    public List<AudioClip> audioClips;
    AudioSource m_MyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (audioClips.Count == 0) return;

        m_MyAudioSource = GetComponent<AudioSource>();

        SelectTrackAndPlay();
    }

    void Update()
    {
        if(!m_MyAudioSource.isPlaying)
        {
            SelectTrackAndPlay();
        }
    }

    void SelectTrackAndPlay()
    {
        int audioClipIndex = Random.Range(0, audioClips.Count - 1);
        m_MyAudioSource.clip = audioClips[audioClipIndex];
        m_MyAudioSource.Play();
    }
    
}
