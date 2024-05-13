using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Keycard : MonoBehaviour
{

    [SerializeField] private KeycardType _keycardType;

    [SerializeField] private int _targetMaterialIndex;

    [Header("COLORS")]
    [SerializeField] private Color _purple;
    [SerializeField] private Color _red;
    [SerializeField] private Color _blue;

    private MeshRenderer _meshRenderer;

    public KeycardType Keycardtype => _keycardType;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        SetColor();
    }

    public void SetType(KeycardType type)
    {
        _keycardType = type;

        SetColor();
    }

    private void SetColor()
    {
        switch(_keycardType) 
        {
            case KeycardType.Purple:
            {
                _meshRenderer.materials[_targetMaterialIndex].color = _purple;
                break;
            }
            case KeycardType.Red:
            {
                _meshRenderer.materials[_targetMaterialIndex].color = _red;
                break;
            }
            case KeycardType.Blue:
            {
                _meshRenderer.materials[_targetMaterialIndex].color = _blue;
                break;
            }

        }
    }
}
