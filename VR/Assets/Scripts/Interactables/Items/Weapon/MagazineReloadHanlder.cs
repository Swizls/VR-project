using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineReloadHanlder : MonoBehaviour
{
    private Gun _weapon;

    private void Start()
    {
        _weapon = GetComponentInParent<Gun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Magazine magazine) && !_weapon.IsMagazineLoaded)
        {
            Destroy(other.gameObject);
            _weapon.Reload();
        }
    }
}
