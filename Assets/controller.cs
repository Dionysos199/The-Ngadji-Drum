using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
public class controller : MonoBehaviour
{
    List<InputDevice> attachedDevices;
    public float MaximumHitForce;

    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initialiseControllers()
    {
        attachedDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, attachedDevices);
        if (attachedDevices.Count > 0)
        {
            targetDevice = attachedDevices[0];
        }
    }
    public void hapticFeedback(float hitForce)
    {
        if (!targetDevice.isValid)
        {
            initialiseControllers();
        }

      
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightController.TryGetFeatureValue(CommonUsages.grip, out float gripValue);

        Debug.Log(gripValue);

        if (gripValue == 1)
        {
            Debug.Log(rightController.characteristics);
            Debug.Log("haptick Feedback");
            float amplitude = 2 + hitForce / (2 * MaximumHitForce);
            float duration = 2 * hitForce / (MaximumHitForce);

            Debug.Log("ampl" + amplitude);
            Debug.Log("duration" + duration);
            HapticCapabilities capabilities;
            if (rightController.TryGetHapticCapabilities(out capabilities))
                if (capabilities.supportsImpulse)
                    rightController.SendHapticImpulse(0, 1, duration);
        }
    }
}
