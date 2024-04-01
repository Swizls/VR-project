using System;
using System.Collections.Generic;

public class ResourceBank
{
    private Dictionary<string, int> _resources = new Dictionary<string, int>();

    public Action BankValuesUpdated;

    public Dictionary<string, int> Resources => _resources;

    public void Add(string name, int amount = 10)
    {
        if(CheckValues(name, amount))
            _resources[name] = amount;

        BankValuesUpdated?.Invoke();
    }
    public void Remove(string name, int amount = 10)
    {
        if(CheckValues(name, amount) && _resources.ContainsKey(name))
            _resources[name] -= amount;

        BankValuesUpdated?.Invoke();
    }

    private bool CheckValues(string name, int amount)
    {
        if(name == null) return false;
        if(amount < 0) return false;

        return true;
    }
}
