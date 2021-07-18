using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAduioController : MonoBehaviour
{

    public AudioClip[] SFXClips;
    public AudioClip[] BGMClips;
    public AudioSource SFXAudioClip;
    public AudioSource BGMAudioSource;

    private void Start() 
    {
        //play the default music when the scene loads
        BGMAudioSource.clip = BGMClips[0];
        BGMAudioSource.Play();
    }

    public void PlayerWinMusic()
    {
        BGMAudioSource.clip = BGMClips[1];
        BGMAudioSource.Play();
    }

    public void PlayerLoseMusic()
    {
        BGMAudioSource.clip = BGMClips[2];
        BGMAudioSource.Play();
    }

    public void PlayerGrappleSFX()
    {
        SFXAudioClip.clip = SFXClips[1];
        SFXAudioClip.Play();
    }

    public void PlayerHitSFX()
    {
        SFXAudioClip.clip = SFXClips[0];
        SFXAudioClip.Play();
    }
}
