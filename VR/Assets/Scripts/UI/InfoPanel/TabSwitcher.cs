using System;
using UnityEngine;

public class TabSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _tabContainer;
    [SerializeField] private GameObject _tab;

    private void Start()
    {
        if (_tabContainer == null)
            throw new NullReferenceException(nameof(_tabContainer) + " " + _tabContainer);

        if (_tab == null)
            throw new NullReferenceException(nameof(_tab) + " " + _tab);
    }

    public void SwitchTab()
    {
        for(int i = 0; i < _tabContainer.transform.childCount; i++) 
        {
            _tabContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        _tab.SetActive(true);
    }
}