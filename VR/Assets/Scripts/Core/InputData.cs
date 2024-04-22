using System.Collections.Generic;
using UnityEngine.XR;

public static class InputData
{
    private static InputDevice _rightController;
    private static InputDevice _leftController;
    private static InputDevice _HMD;

    public static InputDevice LeftController
    {
        get
        {
            InitializeInputDevices();
            return _leftController;
        }
    }
    public static InputDevice RightController
    {
        get
        {
            InitializeInputDevices();
            return _rightController;
        }
    }
    public static InputDevice HMD
    {
        get 
        {
            InitializeInputDevices();
            return _HMD;
        } 
    }

    private static void InitializeInputDevices()
    {
        
        if(!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid) 
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        if (!_HMD.isValid) 
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);

    }

    private static void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }

}
