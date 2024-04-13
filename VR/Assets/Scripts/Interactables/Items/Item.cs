using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private List<ResourceData> _resources = new List<ResourceData>();

    private Rigidbody _rigidbody;

    public string Name => _name;
    public List<ResourceData> Resources => _resources;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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

    private void OnEnable()
    {
        if(_rigidbody != null) 
        { 
            _rigidbody.velocity = Vector3.zero;
        }
    }
}