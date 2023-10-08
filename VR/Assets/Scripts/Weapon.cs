using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _magazineReloadTrigger;
    [SerializeField] private Transform _magazineSpot;

    [SerializeField] private GameObject _magazinePrefab;

    [SerializeField] private AudioClip _weaponShotSound;
    [SerializeField] private AudioClip _weaponReloadSound;
    [SerializeField] private AudioClip _emptyWeaponSound;
    [SerializeField] private AudioClip _weaponEjectedMagazineSound;

    [SerializeField] private bool _hasMagazineOnStart;

    private AudioSource _audioSource;

    private GameObject _loadedMagazine;
    private InputData _inputData;

    public GameObject LoadedMagzine => _loadedMagazine;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _inputData = GetComponent<InputData>();

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Fire);

        if(_hasMagazineOnStart)
        {
            LoadMagazine();
        }
    }

    private void LoadMagazine()
    {
        GameObject magazine = Instantiate(_magazinePrefab, _magazineSpot.transform);

        _loadedMagazine = magazine;

        Destroy(magazine.GetComponent<XRGrabInteractable>());
        Destroy(magazine.GetComponent<Rigidbody>());
        Destroy(magazine.GetComponent<Magazine>());

        _hasMagazineOnStart = magazine;

        _audioSource.clip = _emptyWeaponSound;
        _audioSource.Play();
    }

    private void Update()
    {
        _inputData.RightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed);
        if (isPressed)
        {
            Eject();
        }
    }

    private void Fire(ActivateEventArgs arg0)
    {
        if(_loadedMagazine != null)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

            if(hit.collider.TryGetComponent(out RagdollHandler ragdoll))
            {
                ragdoll.ToggleRagdoll();
            }

            _audioSource.clip = _weaponShotSound;
            _audioSource.Play();
        }
    }

    private void Eject() 
    {
        _loadedMagazine.AddComponent<Rigidbody>();
        _loadedMagazine.transform.SetParent(null);

        _loadedMagazine = null;

        _audioSource.clip = _emptyWeaponSound;
        _audioSource.Play();
    }

    public void Reload()
    {
        if(_loadedMagazine == null) 
            LoadMagazine();
    }
}
