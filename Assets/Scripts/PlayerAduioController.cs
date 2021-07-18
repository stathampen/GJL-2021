using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAduioController : MonoBehaviour
{

    public AudioClip[] SFXClips;
    public AudioSource SFXAudioClip;

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
