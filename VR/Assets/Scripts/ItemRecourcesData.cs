using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default resource list", menuName = "Resources/Resource list")]
public class ItemRecourcesData : ScriptableObject
{
    [SerializeField] private List<ResourceData> _resources = new List<ResourceData>();

    public List<ResourceData> Resources => _resources;
}