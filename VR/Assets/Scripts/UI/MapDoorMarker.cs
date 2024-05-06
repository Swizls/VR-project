using UnityEngine;

public class MapDoorMarker : MonoBehaviour
{
    [SerializeField] private Material _unlockedMaterial;
    [SerializeField] private Material _lockedMaterial;

    private Airlock _airlock;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _airlock = GetComponentInParent<Airlock>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _airlock.LockValueChanged += SetMaterial;

        SetMaterial();
    }

    private void OnDisable()
    {
        _airlock.LockValueChanged -= SetMaterial;
    }

    private void SetMaterial()
    {
        if (_airlock.IsLocked)
            _meshRenderer.material = _lockedMaterial;
        else
            _meshRenderer.material = _unlockedMaterial;
    }
}
