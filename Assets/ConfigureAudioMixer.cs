using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfigureAudioMixer : MonoBehaviour
{

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {        
        audioMixer.SetFloat("MusicVol", Mathf.Log10(GameControl.gameControl.settings.musicVol) * 20);
    }

}
