using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    [SerializeField] AudioClip fireSFX;
    [SerializeField] AudioClip cooldownSFX;
    [SerializeField] AudioClip reloadSFX;

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void GunFired() {
        audioSource.PlayOneShot(fireSFX);
    }

    public void GunCooldown() {
        audioSource.PlayOneShot(cooldownSFX);
    }

    public void GunReloading() {
        audioSource.PlayOneShot(reloadSFX);
    }
}
