using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class show_play_subtitles_sound_trigger : MonoBehaviour
{
    public GameObject uiObject;
    public AudioSource playSound;
    public GameObject danceMusic;
    public lastSceneScript lastscene;

    // Start is called before the first frame update
    void Start()
    {

        //  StartCoroutine(waitBeforeReturning());
        
        playSound.Stop();
    }
    private void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        Debug.Log("TRIGGER VALUE" + triggerValue);
        if (triggerValue == 1)
        {
            lastscene.returnToMuseum();
        }
    }
   
    // Update is called once per frame
    void OnTriggerEnter (Collider player)
    {
        Destroy(gameObject);
            playSound.Play();
            danceMusic.GetComponent<AudioSource>().volume = .2f;
    




    }

    

}
