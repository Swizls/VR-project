using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class InfoPanel : MonoBehaviour
{
    private const string OPENING_ANIMATION_NAME = "Panel_Opening";
    [SerializeField] GameObject _canvas;

    private Animator _animator;

    private bool _isButtonStillPressed = false;

    private void Start()
    {
        _canvas.SetActive(false);
        _animator = GetComponent<Animator>();
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
            _animator.Rebind();
            _animator.Play(OPENING_ANIMATION_NAME);
            _isButtonStillPressed = true;
            _canvas.SetActive(!_canvas.active);
        }
        else
        {
            _isButtonStillPressed = false;
        }
    }
}
