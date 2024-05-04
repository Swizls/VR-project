using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirlockButtons : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private Color _lockedIndicatorColor;
    [SerializeField] private Color _unlockedIndicatorColor;
    [SerializeField] private string _lockedText;
    [SerializeField] private string _unlockedText;

    private Airlock _airlock;

    private List<Image> _buttonImages = new List<Image>();
    private List<TextMeshProUGUI> _buttonTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        _airlock = GetComponentInParent<Airlock>();

        _airlock.LockValueChanged += SetState;

        foreach(Button button in _buttons)
        {
            _buttonImages.Add(button.GetComponent<Image>());
            _buttonTexts.Add(button.GetComponentInChildren<TextMeshProUGUI>());
        }

        SetState();
    }

    private void OnDisable()
    {
       _airlock.LockValueChanged -= SetState;
    }

    private void SetState()
    {
        if(_airlock.IsLocked)
        {
            foreach(Image image in _buttonImages) 
            { 
                image.color = _lockedIndicatorColor;
            }
            foreach (TextMeshProUGUI text in _buttonTexts)
            {
                text.text = _lockedText;
            }
        }
        else
        {
            foreach (Image image in _buttonImages)
            {
                image.color = _unlockedIndicatorColor;
            }
            foreach (TextMeshProUGUI text in _buttonTexts)
            {
                text.text = _unlockedText;
            }
        }
    }
}