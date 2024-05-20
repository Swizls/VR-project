using UnityEngine;
using Utilities.Resources;

namespace SOData
{
    [CreateAssetMenu(fileName = "Craft recipe", menuName = "Craft/Recipe")]
    public class CraftRecipe : ScriptableObject
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private string _recipeName;
        [SerializeField] private ResourceDictionary<string, int> _requiredResources = new();

        public GameObject ItemPrefab => _itemPrefab;
        public string RecipeName => _recipeName;
        public ResourceDictionary<string, int> RequiredResources => _requiredResources;
    }
}
