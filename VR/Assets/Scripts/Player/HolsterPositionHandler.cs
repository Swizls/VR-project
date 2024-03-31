using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterPositionHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Transform _cameraReference;

    [SerializeField] private bool _debug;

    public Vector3 CameraOffset => _cameraOffset;
    public Transform CameraReference => _cameraReference;

    private void Start()
    {
        if (_debug)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = transform.position;
            cube.transform.parent = transform;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    private void Update()
    {
        transform.position = CalculatePositionForRotation();
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
    }

    public Vector3 CalculatePositionForRotation()
    {
        float cameraRotationY = Mathf.Deg2Rad * _cameraReference.eulerAngles.y;

        float offsetX = Mathf.Cos(-cameraRotationY) * _cameraOffset.x;
        float offsetZ = Mathf.Sin(-cameraRotationY) * _cameraOffset.x;

        Vector3 calculatedPosition = _cameraReference.position +
            new Vector3(offsetX, _cameraOffset.y, offsetZ);

        return calculatedPosition;
    }
}
