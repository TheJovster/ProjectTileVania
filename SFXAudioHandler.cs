using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioHandler : MonoBehaviour
{
    public static SFXAudioHandler instance;
    [HideInInspector]public AudioSource sfxPlayer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
    }
}
