using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    [SerializeField] AudioClip bounceFX;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") 
        {
            SFXAudioHandler.instance.sfxPlayer.PlayOneShot(bounceFX);
        }
    }
}
