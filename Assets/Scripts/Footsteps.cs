using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Footsteps : MonoBehaviour
{
    CharacterController cc;
    AudioSource Audio;
    ////GameObject floorTag;
    //public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 padYaxis);
        Debug.Log("padYaxis" + padYaxis);

        //RaycastHit hit;
        //if(Physics.Raycast(this.transform.position,Vector3.down,out hit))
        //{
        //    terrain = hit.collider.tag 
        //}
        //StartCoroutine(WalkSound());



        //the footsteps sound effect is played whenever the value from the touch pad of the controller is greater than .1f
        if ( padYaxis.y > .1f && Audio.isPlaying == false)
        {
            //        //yield return new WaitForSeconds(2.0f);
            //        //Audio.volume = Random.Range(0.8f, 1);
            //        //Audio.pitch = Random.Range(0.8f,1.1f);
            //Audio.Play();
            Audio.volume = Random.Range(0.2f, .5f);
            Audio.pitch = padYaxis.y;
            Audio.PlayDelayed(0.4f);

        }

    }





    //public IEnumerator WalkSound() 
    //{
    //    if (cc.isGrounded == true && cc.velocity.magnitude > 1.0f && Audio.isPlaying == false)
    //    {
    //        //yield return new WaitForSeconds(2.0f);
    //        //Audio.volume = Random.Range(0.8f, 1);
    //        //Audio.pitch = Random.Range(0.8f,1.1f);
    //        //Audio.Play();
    //        Audio.PlayDelayed(0.5f);

    //    }
}
            




