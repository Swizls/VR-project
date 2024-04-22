using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
[RequireComponent(typeof(GunAmmoHandler))]
public class GunEffectsController : MonoBehaviour
{
    private const float MUZZLE_FLASH_DURATION = 0.3f;

    [SerializeField] private GameObject _muzzleFlash;
    [Space]
    [SerializeField] private AudioSource _audioSource;
    [Header("Audio clips")]
    [SerializeField] private AudioClip _weaponShotSound;
    [SerializeField] private AudioClip _weaponReloadSound;
    [SerializeField] private AudioClip _emptyWeaponSound;
    [SerializeField] private AudioClip _weaponEjectedMagazineSound;

    private Gun _gun;
    private GunAmmoHandler _gunAmmoHandler;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _gun = GetComponent<Gun>();
        _gunAmmoHandler = GetComponentInChildren<GunAmmoHandler>();

        if(_muzzleFlash != null)
            _muzzleFlash.SetActive(false);

        _gun.Shot += PlayShotEffects;
        _gunAmmoHandler.ReloadAction += PlayReloadEffects;
        _gunAmmoHandler.EjectAction += PlayEjectEffects;
    }

    private void OnDisable()
    {
        _gun.Shot -= PlayShotEffects;
        _gunAmmoHandler.ReloadAction -= PlayReloadEffects;
        _gunAmmoHandler.EjectAction -= PlayEjectEffects;
    }

    private void PlayEjectEffects()
    {
        SetAudioClipAndPlay(_weaponEjectedMagazineSound);
    }

    private void PlayReloadEffects()
    {
        SetAudioClipAndPlay(_weaponReloadSound);
    }

    public void PlayShotEffects()
    {
        SetAudioClipAndPlay(_weaponShotSound);
        _muzzleFlash.SetActive(true);
        Invoke("DisableMuzzleFlash", MUZZLE_FLASH_DURATION);
    }

    private void DisableMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }

    private void SetAudioClipAndPlay(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
