using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : Weapon
{
    [SerializeField] private Transform _firePoint;
    
    private GunAmmoHandler _ammoHandler;
    private NoiseMaker _noiseMaker;

    public event Action Shot;

    void Start()
    {
        _noiseMaker = GetComponent<NoiseMaker>();
        if(_ammoHandler == null)
            _ammoHandler = GetComponentInChildren<GunAmmoHandler>();
    }

    public void Shoot(ActivateEventArgs arg0)
    {
        if (!_ammoHandler.IsMagazineLoaded)
            return;

        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

        if (hit.collider == null)
            return;

        if (hit.collider.TryGetComponent(out IHitReaction hitable))
            hitable.HitReaction(Damage);

        _noiseMaker.MakeNoise();
        Shot?.Invoke();
    }
}