using SOData;
using UnityEngine;

[RequireComponent(typeof(CraftRecipesContainer))]
public class CraftRecipesRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private CraftRecipeUIPresenter _UIElementPrefab;

    private CraftRecipesContainer _recipesContainer;

    private void Start()
    {
        _recipesContainer = GetComponent<CraftRecipesContainer>();

        Render();
    }

    public void Render()
    {
        foreach (CraftRecipe recipe in _recipesContainer.AvailableCraftRecipe) 
        {
            CraftRecipeUIPresenter element = Instantiate(_UIElementPrefab, _container.transform);
            element.Initialize(_recipesContainer, recipe);
        }
    }
}
