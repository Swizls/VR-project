using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent Pressed;

    public void Press()
    {
        Pressed.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Press();
    }
}
