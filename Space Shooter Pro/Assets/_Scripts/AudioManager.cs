using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioClip _powerupClip;
    [SerializeField]
    private AudioClip _laserShotClip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        _audioSource.clip = _explosionClip;
        _audioSource.Play();
    }

    public void PlayPowerup()
    {
        _audioSource.clip = _powerupClip;
        _audioSource.Play();
    }

    public void PlayLaserShot()
    {
        _audioSource.clip = _laserShotClip;
        _audioSource.Play();
    }
}
