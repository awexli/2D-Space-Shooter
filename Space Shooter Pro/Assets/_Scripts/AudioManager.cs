using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _powerup;
    [SerializeField]
    private AudioClip _laser;
    [SerializeField]
    private AudioClip _playerHit;
    [SerializeField]
    private AudioClip _shieldBreak;
    private AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayPowerup()
    {
        _audio.PlayOneShot(_powerup);
    }

    public void PlayerLaserShot()
    {
        _audio.PlayOneShot(_laser);
    }

    public void PlayerHit()
    {
        _audio.PlayOneShot(_playerHit);
    }

    public void ShieldBreak()
    {
        _audio.PlayOneShot(_shieldBreak);
    }
}
