using UnityEngine;

public class ItemRecycler : MonoBehaviour
{
    [SerializeField] private ResourceBankPresenter _resourcePresenter;

    private void OnTriggerEnter(Collider collider)
    {
        if(CheckIsItem(collider.gameObject, out Item item))
        {
            foreach (ResourceData resource in item.Resources)
            {
                _resourcePresenter.Bank.Add(resource.name);
            }
            if(collider.gameObject.transform.parent != null)
            {
                Destroy(collider.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(collider.gameObject);
            }
        }
    }

    private bool CheckIsItem(GameObject gameObject, out Item item)
    {
        Item component = gameObject.GetComponent<Item>();

        if(component != null)
        {
            item = component;
            return true;
        }

        component = gameObject.GetComponentInParent<Item>();

        if(component != null )
        {
            item = component;
            return true;
        }

        item = null;
        return false;
    }
}
