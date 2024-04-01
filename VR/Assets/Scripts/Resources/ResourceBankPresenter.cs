using UnityEngine;

public class ResourceBankPresenter : MonoBehaviour
{
    private ResourceBank _bank = new ResourceBank();

    public ResourceBank Bank => _bank;
}
