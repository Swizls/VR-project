using UnityEngine;

[CreateAssetMenu(fileName = "Default resource data", menuName = "Resources/Resource data")]
public class ResourceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public Sprite Icon => _icon;
}