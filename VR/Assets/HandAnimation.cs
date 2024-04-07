using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class HandAnimation : MonoBehaviour
{
    private enum HandType
    {
        Left,
        Right
    }
    [SerializeField] private HandType handType;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(handType == HandType.Left) 
        {
            if (InputData.LeftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
                _animator.SetFloat("Trigger", triggerValue);
            if(InputData.LeftController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                _animator.SetFloat("Grip", gripValue);
        }
        else
        {
            if (InputData.RightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
                _animator.SetFloat("Trigger", triggerValue);
            if (InputData.RightController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                _animator.SetFloat("Grip", gripValue);
        }
    }
}
