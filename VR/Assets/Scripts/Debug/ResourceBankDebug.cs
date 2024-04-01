using System.Linq;
using UnityEngine;

public class ResourceBankDebug : MonoBehaviour
{
    [SerializeField] private bool _debug;

    private ResourceBankPresenter _presenter;

    private void Start()
    {
        if (!_debug)
            return;

        _presenter = FindAnyObjectByType<ResourceBankPresenter>();

        _presenter.Bank.BankValuesUpdated += Debug;
    }

    private void OnDisable()
    {
        _presenter.Bank.BankValuesUpdated -= Debug;
    }

    private void Debug()
    {
        for(int i = 0; i < _presenter.Bank.Resources.Count; i++)
            UnityEngine.Debug.Log(_presenter.Bank.Resources.ElementAt(i).Key + ": " + _presenter.Bank.Resources.ElementAt(i).Value);
    }
}