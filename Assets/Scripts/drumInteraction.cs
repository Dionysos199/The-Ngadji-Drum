using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

[System.Serializable]
public class hitdrum : UnityEvent<float, float, float>
{

}
public class drumInteraction : MonoBehaviour
{

    [SerializeField] private hitdrum hitDrum;



    //public Transform explosionPrefab;
    AudioSource drumSound;
    public Transform stickHead;
    Transform hidePos;
    Rigidbody stickHeadRb;
    // Start is called before the first frame update
    public float MaximumHitForce;
    private InputDevice targetDevice;

    public ParticleSystem godray;

    public GameObject soundObject;
    public AudioClip[] sounds;


    float lastTime;

    List<InputDevice> attachedDevices;

    [SerializeField] private Material myMaterial;

    float numberOfHits;

    void Start()
    {
        lastTime = Time.time;
        hidePos = GetComponentInParent<Transform>();
        drumSound = GetComponent<AudioSource>();
        stickHeadRb = stickHead.GetComponent<Rigidbody>();

        godray = godray.GetComponent<ParticleSystem>();

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


            if (collision.collider.name == "stickHead")
            {
                numberOfHits++;


                // get the speed of the drumstick the moment it hits the drum 
                float hitForce = stickHeadRb.GetPointVelocity(transform.TransformPoint(stickHead.position)).magnitude;

                ContactPoint contact = collision.GetContact(0);
                Vector3 hitPos = contact.point;

                //calculates how far from the center of the drum we are hitting to use it
                // to control the pitch of the emitted sound

                float distFromCenter = Vector3.Distance(hitPos, hidePos.position);


                hitDrum?.Invoke(hitForce, distFromCenter, numberOfHits);


                playDrumSound(hitPos, hitForce, distFromCenter);

                hapticFeedback(hitForce);

                myMaterial.color = new Color(5 * distFromCenter, 0, 0);

                //particle system reactiom
                var emission = godray.emission;
                emission.rateOverTime = Mathf.Pow(hitForce, 4);
            }
        }
        lastTime = Time.fixedTime;

    }
    void hapticFeedback(float hitForce)
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
    void playDrumSound(Vector3 position, float hitForce, float distanceFromCenter)
    {

        GameObject newSoundObject = Instantiate(soundObject, position, Quaternion.identity);
        drumSound = newSoundObject.GetComponent<AudioSource>();

        drumSound.clip = sounds[0];

        drumSound.pitch = (distanceFromCenter * 3);
        drumSound.volume = hitForce;
        drumSound.Play();
        Destroy(newSoundObject, 4);

    }

}
