using UnityEngine;

public class Keycard : MonoBehaviour
{

    [SerializeField] private KeycardType _keycardType;

    public KeycardType Keycardtype => _keycardType;
}
