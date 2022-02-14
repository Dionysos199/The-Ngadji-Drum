using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

[System.Serializable]
public class hitdrum : UnityEvent<float, Vector3, int>
{

}
public class drumInteraction : MonoBehaviour
{

    [SerializeField] private hitdrum hitDrum;



    //public Transform explosionPrefab;

    public Transform stickHead;
     Transform hidePos;
    Rigidbody stickHeadRb;
    // Start is called before the first frame update
    public float MaximumHitForce;
    private InputDevice targetDevice;




    float lastTime;

    List<InputDevice> attachedDevices;


    int numberOfHits;
    int phaseNum;

    void Start()
    {
        lastTime = Time.time;
        stickHeadRb = stickHead.GetComponent<Rigidbody>();


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
    void OnCollisionEnter(Collision collision)
    {
        if (Time.fixedTime - lastTime > .1)
        {
            gameManager gameInstance =gameManager.Instance;

            if (collision.collider.name == "stickHead")
            {
                numberOfHits++;

                if (numberOfHits % 3 == 0)
                {
                    phaseNum++;
                    gameManager.Phase = gameManager.Phases[phaseNum];
                }

                // get the speed of the drumstick the moment it hits the drum 
                float hitForce = stickHeadRb.GetPointVelocity(transform.TransformPoint(stickHead.position)).magnitude/100;

                ContactPoint contact = collision.GetContact(0);
                Vector3 hitPos = contact.point;

                Debug.Log("hitPos"+hitPos);
                //calculates how far from the center of the drum we are hitting to use it
                // to control the pitch of the emitted sound




                hitDrum?.Invoke(hitForce, hitPos, numberOfHits);



                hapticFeedback(hitForce);

                // myMaterial.color = new Color(5 * distFromCenter, 0, 0);
                
            }
        }
        lastTime = Time.fixedTime;

    }
    void hapticFeedback(float hitForce)
    {
        Debug.Log("hitforce  "+hitForce);
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
