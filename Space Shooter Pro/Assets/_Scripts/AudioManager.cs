using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _powerupSound;
    void Start()
    {
        _powerupSound = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        _powerupSound.Play();
    }
}
