using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject _canvas;

    private bool _isButtonStillPressed = false;

    private void Start()
    {
        _canvas.SetActive(false);
    }

    private void Update()
    {
        ToggleCanvas();
    }

    private void ToggleCanvas()
    {
        InputData.LeftController.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed);
        if (isPressed)
        {
            if (_isButtonStillPressed)
            {
                return;
            }
            _isButtonStillPressed = true;
            _canvas.SetActive(!_canvas.active);
        }
        else
        {
            _isButtonStillPressed = false;
        }
    }
}
