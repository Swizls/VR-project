using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public enum KeycardType
    {
        Purple,
        Blue,
        Red
    }

    [SerializeField] private KeycardType _keycardType;

    public KeycardType Keycardtype => _keycardType;
}
