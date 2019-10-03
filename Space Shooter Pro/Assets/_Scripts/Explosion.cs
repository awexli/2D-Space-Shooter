using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _playExplosionClip;

    void Start()
    {
        _playExplosionClip = GetComponent<AudioSource>();
        _playExplosionClip.Play();
        Destroy(this.gameObject, 2.3f);
    }
}
