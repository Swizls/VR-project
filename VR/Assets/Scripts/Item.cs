using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private List<ResourceData> _resources = new List<ResourceData>();

    public string Name => _name;
    public List<ResourceData> Resources => _resources;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Display(Transform transform = null)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
    }
}