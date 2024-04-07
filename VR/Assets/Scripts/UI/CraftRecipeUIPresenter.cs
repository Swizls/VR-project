using SOData;
using TMPro;
using UnityEngine;

public class CraftRecipeUIPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;

    private CraftRecipesContainer _recipesContainer;
    private CraftRecipe _craftRecipe;

    public void Initialize(CraftRecipesContainer recipesContainer, CraftRecipe recipe)
    {
        _recipesContainer = recipesContainer;
        _craftRecipe = recipe;

        _title.text = _craftRecipe.name;
    }

    public void OnCraftSelect()
    {
        if (_craftRecipe == null)
            throw new System.NullReferenceException();
            
        _recipesContainer.SelectCraftRecipe(_craftRecipe);
    }
}
