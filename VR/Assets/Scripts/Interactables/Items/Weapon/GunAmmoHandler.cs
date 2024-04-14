using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunAmmoHandler : MonoBehaviour
{
    [SerializeField] private Transform _magazineReloadTrigger;
    [SerializeField] private Transform _magazineSpot;
    [SerializeField] private GameObject _magazinePrefab;
    [SerializeField] private bool _hasMagazineOnStart;

    private Gun _weapon;
    private GameObject _loadedMagazine;
    private bool _isMagazineLoaded;

    private int _currentAmmoCount;

    public event Action ReloadAction;
    public event Action EjectAction;

    public bool IsMagazineLoaded => _isMagazineLoaded;

    private void Start()
    {
        _weapon = GetComponentInParent<Gun>();

        if (_hasMagazineOnStart)
            LoadMagazine();
    }
    private void Update()
    {
        InputData.RightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed);
        if (isPressed)
        {
            Eject();
        }
    }

    private void Eject()
    {
        if (!_isMagazineLoaded)
            return;

        if (_loadedMagazine != null)
        {
            _loadedMagazine.AddComponent<Rigidbody>();
            _loadedMagazine.transform.SetParent(null);

            _loadedMagazine = null;
        }
        else
        {
            GameObject magazine = Instantiate(_magazinePrefab, transform.position, transform.rotation);
        }

        _isMagazineLoaded = false;
        EjectAction?.Invoke();
    }

    public void Reload()
    {
        if (_isMagazineLoaded)
            return;
        
        LoadMagazine();
        ReloadAction?.Invoke();
    }
    private void LoadMagazine()
    {
        if (_magazineSpot != null)
        {
            GameObject magazine = Instantiate(_magazinePrefab, _magazineSpot.transform);

            _loadedMagazine = magazine;

            Destroy(magazine.GetComponent<XRGrabInteractable>());
            Destroy(magazine.GetComponent<Rigidbody>());
            Destroy(magazine.GetComponent<Magazine>());

            _hasMagazineOnStart = magazine;
        }

        _isMagazineLoaded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Magazine magazine) && !IsMagazineLoaded)
        {
            Destroy(other.gameObject);
            Reload();
        }
    }
}
