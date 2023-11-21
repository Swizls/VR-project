using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineReloadHanlder : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponentInParent<Weapon>();
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
