using UnityEngine;

public class BaseItem : MonoBehaviour
{
    [SerializeField] private string _name;

    public string Name => _name;

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